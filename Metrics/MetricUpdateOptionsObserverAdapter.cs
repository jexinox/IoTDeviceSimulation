using System;
using System.Reactive;
using Microsoft.Extensions.Options;

namespace IoTDeviceSimulation.Metrics;

public class MetricUpdateOptionsObserverAdapter : IOptions<MetricUpdateOptions>, IObserver<MetricUpdateOptions>
{
    private readonly Lazy<IObserver<MetricUpdateOptions>> _internalObserver;

    public MetricUpdateOptionsObserverAdapter()
    {
        _internalObserver = new(() => Observer.Create<MetricUpdateOptions>(options => Value = options));
    }

    public MetricUpdateOptions Value { get; private set; } = 
        new(MetricUpdateOptionsViewModel.DefaultSecondsBetweenUpdates);
    
    public void OnCompleted() => _internalObserver.Value.OnCompleted();

    public void OnError(Exception error) => _internalObserver.Value.OnError(error);

    public void OnNext(MetricUpdateOptions value) => _internalObserver.Value.OnNext(value);
}