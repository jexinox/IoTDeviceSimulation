using System;
using System.Reactive.Linq;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public class MetricActuatorOperator(IObservable<IActuatorOptions> options, IActuatorFactory actuatorFactory)
{
    public IObservable<Metric> Apply(IObservable<Metric> metrics)
    {
        return metrics
            .WithLatestFrom(options)
            .Select(tuple => Actuate(tuple.First, tuple.Second));
    }

    private Metric Actuate(Metric metric, IActuatorOptions actuatorOptions)
    {
        return actuatorOptions.Get(actuatorFactory).Actuate(metric);
    }
}