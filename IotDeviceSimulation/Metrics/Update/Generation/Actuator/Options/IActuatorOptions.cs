namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

public interface IActuatorOptions
{ 
    IActuator Get(IActuatorFactory factory);
}