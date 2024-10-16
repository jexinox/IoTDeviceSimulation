namespace IoTDeviceSimulation.Metrics.Update.Generation.Options;

public interface IMetricGeneratorOptions
{
    IMetricGenerator Accept(IMetricGeneratorFactory factory);
}