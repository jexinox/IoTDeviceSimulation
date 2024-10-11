namespace IoTDeviceSimulation.Metrics.Generation;

public interface IMetricGeneratorProvider
{
    IMetricGenerator Get();
}