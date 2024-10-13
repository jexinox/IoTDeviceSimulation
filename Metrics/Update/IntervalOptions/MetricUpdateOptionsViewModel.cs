using System;
using System.Reactive.Linq;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics.Update.IntervalOptions;

public class MetricUpdateOptionsViewModel : ReactiveObject, IObservable<MetricUpdateIntervalOptions>
{
    private readonly Lazy<IObservable<MetricUpdateIntervalOptions>> _internalObservable;

    private TimeSpan _secondsBetweenUpdates;

    public MetricUpdateOptionsViewModel(IDefaultsProvider<MetricUpdateIntervalOptions> metricUpdateOptionsDefaultsProvider)
    {
        _secondsBetweenUpdates = metricUpdateOptionsDefaultsProvider.Get().IntervalBetweenUpdates;
        _internalObservable = new(() => 
            this
                .WhenAnyValue(viewModel => viewModel.SecondsBetweenUpdates)
                .Select(delay => new MetricUpdateIntervalOptions(delay)));
    }
    
    public int SecondsBetweenUpdates
    {
        get => _secondsBetweenUpdates.Seconds;
        set => this.RaiseAndSetIfChanged(ref _secondsBetweenUpdates, TimeSpan.FromSeconds(value));
    }

    public IDisposable Subscribe(IObserver<MetricUpdateIntervalOptions> observer) 
        => _internalObservable.Value.Subscribe(observer);
}