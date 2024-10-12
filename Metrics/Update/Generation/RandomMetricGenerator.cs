using System;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class RandomMetricGenerator(Random random) : IMetricGenerator
{
    public MetricGeneratorType Type => MetricGeneratorType.Random;

    public Metric Generate(Metric metric)
    {
        return new(random.NextDouble());
    }
}