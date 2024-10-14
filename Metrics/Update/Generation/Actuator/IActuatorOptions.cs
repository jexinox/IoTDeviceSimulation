namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public interface IActuatorOptions
{
    public IActuator Get(IActuatorFactory factory);
}