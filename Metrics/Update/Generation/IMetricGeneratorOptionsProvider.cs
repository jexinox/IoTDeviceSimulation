using IoTDeviceSimulation.Metrics.Update.Generation.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public interface IMetricGeneratorOptionsProvider
{
    MetricGeneratorOptions Get();
}