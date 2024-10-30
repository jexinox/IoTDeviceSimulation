using System;
using IoTDeviceSimulation.Metrics.Update.Generation.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class MetricGeneratorFactory : IMetricGeneratorFactory
{
    public IMetricGenerator GetLinearGenerator(LinearMetricGeneratorOptions options) => new LinearMetricGenerator(options);
    
    public IMetricGenerator GetLinearRandomGenerator(LinearRandomMetricGeneratorOptions options)
        => new LinearRandomMetricGenerator(Random.Shared, options);
}