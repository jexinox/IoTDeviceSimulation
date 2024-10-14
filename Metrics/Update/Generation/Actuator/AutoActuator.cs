using System;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public class AutoActuator(AutoActuatorOptions options) : IActuator
{
    public Metric Actuate(Metric metric)
    {
        Console.WriteLine($"Actuator called auto {metric.Value}");
        return metric.Value > options.MetricValueLimit
            ? new(metric.Value - options.MetricChange)
            : metric;
    }
}