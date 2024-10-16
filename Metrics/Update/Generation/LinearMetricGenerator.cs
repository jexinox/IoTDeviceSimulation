using IoTDeviceSimulation.Metrics.Update.Generation.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class LinearMetricGenerator(LinearMetricGeneratorOptions options) : IMetricGenerator
{
    public Metric Generate(Metric metric)
    {
        return new(metric.Value + options.Increment);
    }
}