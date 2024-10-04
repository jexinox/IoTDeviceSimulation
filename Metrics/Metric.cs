namespace IoTDeviceSimulation.Metrics;

public record Metric(double Value)
{
    public static readonly Metric Default = new(MetricViewModel.DefaultMetricValue);
}
