using System.Reactive;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Manual;

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
        if (!_manualHandleEventRaised)
        {
            return metric;
        }
        
        lock (lockObject)
        {
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