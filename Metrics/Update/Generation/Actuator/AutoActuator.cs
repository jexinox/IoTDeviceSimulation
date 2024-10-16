using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public class AutoActuator(AutoActuatorOptions options) : IActuator
{
    public Metric Actuate(Metric metric)
    {
        return metric.Value > options.MetricValueLimit
            ? new(metric.Value - options.MetricChange)
            : metric;
    }
}