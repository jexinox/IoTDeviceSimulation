namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public interface IActuator
{
    Metric Actuate(Metric metric);
}