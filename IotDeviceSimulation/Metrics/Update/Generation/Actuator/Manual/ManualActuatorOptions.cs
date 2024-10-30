using System;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Manual;

public record ManualActuatorOptions(IObservable<ManualActuatorHandleEvent> ManualEvents) : IActuatorOptions
{
    public IActuator Get(IActuatorFactory factory) => factory.GetManualActuator(this);
}