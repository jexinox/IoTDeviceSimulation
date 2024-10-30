using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Auto;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Manual;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Mqtt;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public interface IActuatorFactory
{
    IActuator GetManualActuator(ManualActuatorOptions actuatorOptions);
    
    IActuator GetAutoActuator(AutoActuatorOptions actuatorOptions);
    
    IActuator GetMqttAutoActuator(MqttAutoActuatorOptions mqttAutoActuatorOptions);
}