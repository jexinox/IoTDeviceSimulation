using IoTDeviceSimulation.Metrics.Update.Generation.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public interface IMetricGeneratorFactory
{
    IMetricGenerator Create(MetricGeneratorOptions options);
}