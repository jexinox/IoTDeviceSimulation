using System.Threading.Tasks;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Auto;

public record AutoActuatorOptions(double MetricValueLimit = 0.7, double MetricChange = 0.2) : IActuatorOptions
{
    public Task<IActuator> Get(IActuatorFactory factory) => Task.FromResult(factory.GetAutoActuator(this));
}