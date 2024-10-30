using IoTDeviceSimulation.Metrics.Update.Generation.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public interface IMetricGeneratorFactory
{
    IMetricGenerator GetLinearGenerator(LinearMetricGeneratorOptions options);
    
    IMetricGenerator GetLinearRandomGenerator(LinearRandomMetricGeneratorOptions options);
}