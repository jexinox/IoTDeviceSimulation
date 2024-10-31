using System;
using System.Threading.Tasks;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Manual;

public record ManualActuatorOptions(IObservable<ManualActuatorHandleEvent> ManualEvents) : IActuatorOptions
{
    public Task<IActuator> Get(IActuatorFactory factory) => Task.FromResult(factory.GetManualActuator(this));
}