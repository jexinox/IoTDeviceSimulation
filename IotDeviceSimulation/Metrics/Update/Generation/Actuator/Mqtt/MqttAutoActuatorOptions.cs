using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Mqtt;

public record MqttAutoActuatorOptions(
    string ClientId,
    string Topic) : IActuatorOptions
{
    public IActuator Get(IActuatorFactory factory) => factory.GetMqttAutoActuator(this);
}