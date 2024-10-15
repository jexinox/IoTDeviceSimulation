using System;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

public record ManualActuatorOptions(IObservable<ManualActuatorHandleEvent> ManualEvents) : IActuatorOptions
{
    public IActuator Get(IActuatorFactory factory) => factory.GetManualActuator(this);
}