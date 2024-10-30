namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public class ActuatorMetricGeneratorAdapter(
    IActuator actuator, IMetricGenerator metricGenerator) : IMetricGenerator
{
    public Metric Generate(Metric metric)
    {
        var actuated = actuator.Actuate(metric);
        return metricGenerator.Generate(actuated);
    }
}