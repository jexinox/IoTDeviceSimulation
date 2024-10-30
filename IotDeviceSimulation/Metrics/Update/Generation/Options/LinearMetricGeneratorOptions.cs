namespace IoTDeviceSimulation.Metrics.Update.Generation.Options;

public record LinearMetricGeneratorOptions(double Increment = 0.1) : IMetricGeneratorOptions
{
    public IMetricGenerator Accept(IMetricGeneratorFactory factory) => factory.GetLinearGenerator(this);
}