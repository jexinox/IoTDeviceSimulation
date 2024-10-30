using System;
using System.Reactive.Linq;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics.Update.Options;

public class MetricUpdateOptionsViewModel : ReactiveObject, IObservable<MetricUpdateOptions>
{
    private readonly IObservable<MetricUpdateOptions> _internalObservable;

    private TimeSpan _secondsBetweenUpdates = TimeSpan.FromSeconds(5);

    public MetricUpdateOptionsViewModel()
    {
        _internalObservable = this
            .WhenAnyValue(viewModel => viewModel.SecondsBetweenUpdates)
            .Select(delay => new MetricUpdateOptions(delay));
    }
    
    public int SecondsBetweenUpdates
    {
        get => _secondsBetweenUpdates.Seconds;
        set => this.RaiseAndSetIfChanged(ref _secondsBetweenUpdates, TimeSpan.FromSeconds(value));
    }

    public IDisposable Subscribe(IObserver<MetricUpdateOptions> observer) 
        => _internalObservable.Subscribe(observer);
}