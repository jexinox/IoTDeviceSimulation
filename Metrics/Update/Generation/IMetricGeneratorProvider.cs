namespace IoTDeviceSimulation.Metrics.Update.Generation;

public interface IMetricGeneratorProvider
{
    IMetricGenerator Get();
}