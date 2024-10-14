namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public record AutoActuatorOptions(double MetricValueLimit, double MetricChange) : IActuatorOptions
{
    public IActuator Get(IActuatorFactory factory) => factory.GetAutoActuator(this);
}