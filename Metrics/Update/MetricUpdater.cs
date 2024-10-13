using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using IoTDeviceSimulation.Subscriptions;

namespace IoTDeviceSimulation.Metrics.Update;

public class MetricUpdater(IDefaultsProvider<Metric> metricDefaultsProvider) 
    : IObservable<Metric>, IObserver<MetricUpdateOptions>
{
    private readonly SubscriptionsContainer<Metric> _subscribers = new();
    
    private CompositeDisposable _internalObservableSubscriptions = new();
    private CancellationTokenSource _cancellationTokenSource = new();
    private IObservable<Metric> _internalObservable = new[] { metricDefaultsProvider.Get() }.ToObservable();
    
    // NOTE: If we need to unsubscribe from here, also need to dispose internalSub,
    // now it will be fully unsubscribed only when options are updated
    public IDisposable Subscribe(IObserver<Metric> observer)
    {
        _internalObservableSubscriptions.Add(_internalObservable.Subscribe(observer));
        return _subscribers.Subscribe(observer); 
    }

    public void OnCompleted()
    {
        foreach (var subscriber in _subscribers.Observers)
        {
            subscriber.OnCompleted();
        }
        
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
        
        _internalObservableSubscriptions.Dispose();
        
        _subscribers.UnsubscribeAll();
    }

    public void OnError(Exception error)
    {
        foreach (var subscriber in _subscribers.Observers)
        {
            subscriber.OnError(error);
        }
    }

    public void OnNext(MetricUpdateOptions newOptions)
    {
        RenewInternalObservableSubscriptions();
        RenewCancellationTokenSource();
        RenewInternalObservableWithNewOptions(newOptions);
    }

    private void RenewInternalObservableSubscriptions()
    {
        _internalObservableSubscriptions.Dispose();
        _internalObservableSubscriptions = new();
    }

    private void RenewCancellationTokenSource()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
        _cancellationTokenSource = new();
    }

    private void RenewInternalObservableWithNewOptions(MetricUpdateOptions newOptions)
    {
        _internalObservable = _internalObservable
            .LastAsync()
            .Select(metric => new MetricUpdateState(metric, newOptions, _cancellationTokenSource.Token))
            .SelectMany(CreateNewGenerator);
        
        ResubscribeSubscribers();
    }

    private void ResubscribeSubscribers()
    {
        foreach (var subscriber in _subscribers.Observers)
        {
            _internalObservableSubscriptions.Add(_internalObservable.Subscribe(subscriber));
        }
    }

    private static IObservable<Metric> CreateNewGenerator(MetricUpdateState initialState)
    {
        return Observable
            .Generate(
                initialState,
                state => !state.CancellationToken.IsCancellationRequested,
                state => state with { Metric = state.Options.Generator.Generate(state.Metric) },
                state => state.Metric,
                state => state.Options.IntervalOptions.IntervalBetweenUpdates);
    }

    private record MetricUpdateState(Metric Metric, MetricUpdateOptions Options, CancellationToken CancellationToken);
}