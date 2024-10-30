namespace IoTDeviceSimulation.Metrics.Update.Generation.Options;

public record LinearRandomMetricGeneratorOptions(double MinIncrement = 0.1, double MaxIncrement = 0.2) : IMetricGeneratorOptions
{
    public IMetricGenerator Accept(IMetricGeneratorFactory factory) => factory.GetLinearRandomGenerator(this);
}