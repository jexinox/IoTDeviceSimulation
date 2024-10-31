using System.Threading.Tasks;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Mqtt;

public record MqttActuatorOptions(
    string ClientId = "IoTDeviceSimulation.MqttActuator",
    string Topic = "a.karpov/changes",
    string Host = "test.mosquitto.org",
    int Port = 1883) : IActuatorOptions
{
    public Task<IActuator> Get(IActuatorFactory factory) => factory.GetMqttActuator(this);
}