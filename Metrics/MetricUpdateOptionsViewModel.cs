using System;
using ReactiveUI;
using System.Reactive.Linq;

namespace IoTDeviceSimulation.Metrics;

public class MetricUpdateOptionsViewModel : ReactiveObject, IObservable<MetricUpdateOptions>
{
    public const int DefaultSecondsBetweenUpdates = 2;
    
    private readonly Lazy<IObservable<MetricUpdateOptions>> _internalObservable;

    private int _secondsBetweenUpdates = DefaultSecondsBetweenUpdates;

    public MetricUpdateOptionsViewModel()
    {
        _internalObservable = new(() => 
            this
                .WhenAnyValue(viewModel => viewModel.SecondsBetweenUpdates)
                .Select(delay => new MetricUpdateOptions(delay)));
    }
    
    public int SecondsBetweenUpdates
    {
        get => _secondsBetweenUpdates;
        set => this.RaiseAndSetIfChanged(ref _secondsBetweenUpdates, value);
    }

    public IDisposable Subscribe(IObserver<MetricUpdateOptions> observer) => _internalObservable.Value.Subscribe(observer);
}