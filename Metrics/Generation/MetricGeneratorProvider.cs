using System;

namespace IoTDeviceSimulation.Metrics.Generation;

public class MetricGeneratorProvider : IMetricGeneratorProvider
{
    public IMetricGenerator Get()
    {
        return new RandomMetricGenerator(new Random());
    }
}