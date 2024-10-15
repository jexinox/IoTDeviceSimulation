namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

public interface IActuatorOptions
{
    public IActuator Get(IActuatorFactory factory);
}