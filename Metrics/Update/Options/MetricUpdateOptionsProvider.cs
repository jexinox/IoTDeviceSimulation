using System;
using System.Reactive;

namespace IoTDeviceSimulation.Metrics.Update.Options;

public class MetricUpdateOptionsProvider : IMetricUpdateOptionsProvider, IObserver<MetricUpdateOptions>
{
    private readonly Lazy<IObserver<MetricUpdateOptions>> _internalObserver;
    private MetricUpdateOptions _value;

    public MetricUpdateOptionsProvider(IDefaultsProvider<MetricUpdateOptions> defaultOptionsProvider)
    {
        _value = defaultOptionsProvider.Get();
        _internalObserver = new(() => Observer.Create<MetricUpdateOptions>(options => _value = options));
    }
    
    public MetricUpdateOptions Get() => _value;

    public void OnCompleted() => _internalObserver.Value.OnCompleted();

    public void OnError(Exception error) => _internalObserver.Value.OnError(error);

    public void OnNext(MetricUpdateOptions value) => _internalObserver.Value.OnNext(value);
}

public interface IMetricUpdateOptionsProvider
{
    MetricUpdateOptions Get();
}