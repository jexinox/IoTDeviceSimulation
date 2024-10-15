using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public interface IActuatorFactory
{
    IActuator GetManualActuator(ManualActuatorOptions actuatorOptions);
    
    IActuator GetAutoActuator(AutoActuatorOptions actuatorOptions);
}