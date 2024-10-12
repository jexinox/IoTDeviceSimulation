using System;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class MetricGeneratorFactory(Random random) : IMetricGeneratorFactory
{
    public IMetricGenerator Create(MetricGeneratorOptions options)
    {
        return options.Type switch
        {
            MetricGeneratorType.Random => new RandomMetricGenerator(random),
            _ => throw new ArgumentOutOfRangeException(nameof(options), options.Type, null)
        };
    }
}