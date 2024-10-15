using System.Reactive;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public class ManualActuator : IActuator
{
    private readonly object lockObject = new();
        
    private bool _manualHandleEventRaised = false;
    private double _manualHandlesMetricChange = 0;
        
    public ManualActuator(ManualActuatorOptions manualActuatorOptions)
    {
        manualActuatorOptions.ManualEvents.Subscribe(Observer.Create<ManualActuatorHandleEvent>(HandleManualActuatorHandleEvent));
    }

    public Metric Actuate(Metric metric)
    {
        lock (lockObject)
        {
            if (!_manualHandleEventRaised)
            {
                return metric;
            }
                
            var returnValue = new Metric(metric.Value - _manualHandlesMetricChange);
            _manualHandleEventRaised = false;
            _manualHandlesMetricChange = 0;
            return returnValue;
        }
    }

    private void HandleManualActuatorHandleEvent(ManualActuatorHandleEvent manualActuatorHandleEvent)
    {
        lock (lockObject)
        {
            _manualHandleEventRaised = true;
            _manualHandlesMetricChange += manualActuatorHandleEvent.ChangeValue;
        }
    }
}