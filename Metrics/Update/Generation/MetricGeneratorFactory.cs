using System;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class MetricGeneratorFactory(RandomMetricGenerator metricGenerator) : IMetricGeneratorFactory
{
    public IMetricGenerator Create(MetricGeneratorOptions options)
    {
        return options.Type switch
        {
            MetricGeneratorType.Random => metricGenerator,
            _ => throw new ArgumentOutOfRangeException(nameof(options), options.Type, null)
        };
    }
}