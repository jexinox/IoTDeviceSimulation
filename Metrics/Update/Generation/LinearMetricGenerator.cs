namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class LinearMetricGenerator : IMetricGenerator
{
    public Metric Generate(Metric metric)
    {
        return new(metric.Value + 0.1);
    }
}