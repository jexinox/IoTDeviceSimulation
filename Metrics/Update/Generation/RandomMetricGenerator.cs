using System;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class RandomMetricGenerator(Random random) : IMetricGenerator
{
    public Metric Generate(Metric metric)
    {
        return new(random.NextDouble());
    }
}