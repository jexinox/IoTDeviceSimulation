namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public class ActuatorMetricGeneratorAdapter(
    IActuator actuator, IMetricGenerator metricGenerator) : IMetricGenerator
{
    public Metric Generate(Metric metric)
    {
        var generatedMetric = metricGenerator.Generate(metric);
        return actuator.Actuate(generatedMetric);
    }
}