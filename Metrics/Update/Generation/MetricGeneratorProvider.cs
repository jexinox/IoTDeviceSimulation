using System;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class MetricGeneratorProvider(
    IMetricGeneratorFactory factory,
    IMetricGeneratorOptionsProvider optionsProvider) : IMetricGeneratorProvider
{
    public IMetricGenerator Get()
    {
        var options = optionsProvider.Get();
        return factory.Create(options);
    }
}