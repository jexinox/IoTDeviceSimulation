using System;
using System.Reactive;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Options;

public class MetricGeneratorOptionsProvider : IMetricGeneratorOptionsProvider, IObserver<MetricGeneratorOptions>
{
    private readonly Lazy<IObserver<MetricGeneratorOptions>> _internalObserver;
    
    private MetricGeneratorOptions _options;
    
    public MetricGeneratorOptionsProvider(IDefaultsProvider<MetricGeneratorOptions> defaultsProvider)
    {
        _options = defaultsProvider.Get();
        _internalObserver = new(() => Observer.Create<MetricGeneratorOptions>(options => _options = options));
    }

    public MetricGeneratorOptions Get() => _options;

    public void OnCompleted() => _internalObserver.Value.OnCompleted();

    public void OnError(Exception error) => _internalObserver.Value.OnError(error);

    public void OnNext(MetricGeneratorOptions value) => _internalObserver.Value.OnNext(value);
}