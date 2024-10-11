using System;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class MetricGeneratorProvider : IMetricGeneratorProvider
{
    public IMetricGenerator Get()
    {
        return new RandomMetricGenerator(new Random());
    }
}