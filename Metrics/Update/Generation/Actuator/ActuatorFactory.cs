using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public class ActuatorFactory : IActuatorFactory
{
    public IActuator GetManualActuator(ManualActuatorOptions actuatorOptions)
    {
        return new ManualActuator(actuatorOptions);
    }

    public IActuator GetAutoActuator(AutoActuatorOptions actuatorOptions)
    {
        return new AutoActuator(actuatorOptions);
    }
}