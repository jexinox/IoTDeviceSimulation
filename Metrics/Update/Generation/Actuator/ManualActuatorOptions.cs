namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public record ManualActuatorOptions : IActuatorOptions
{
    public IActuator Get(IActuatorFactory factory) => factory.GetManualActuator(this);
}