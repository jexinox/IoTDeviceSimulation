using System.Threading.Tasks;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Mqtt;

public record MqttActuatorOptions(string Topic = "a.karpov/changes") : IActuatorOptions
{
    public Task<IActuator> Get(IActuatorFactory factory) => factory.GetMqttActuator(this);
}