namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Mqtt;

public class MqttActuator : IActuator
{
    private readonly object locker = new();
    private double changes = 0;
    
    public Metric Actuate(Metric metric)
    {
        lock (locker)
        {
            var newMetric = new Metric(metric.Value - changes);
            changes = 0;
            return newMetric;
        }
    }

    public void OnMetricChangesRecieved(MqttMetricChange metricChange)
    {
        lock (locker)
            changes += metricChange.Change;
    }
}