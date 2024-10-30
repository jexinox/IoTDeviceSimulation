using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Auto;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Manual;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Mqtt;

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

    public IActuator GetMqttAutoActuator(MqttAutoActuatorOptions mqttAutoActuatorOptions)
    {
        return new 
    }
}