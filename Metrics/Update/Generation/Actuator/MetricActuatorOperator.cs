using System;
using System.Reactive.Linq;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public class MetricActuatorOperator(IObservable<IActuatorOptions> options, IActuatorFactory factory)
{
    public IObservable<IMetricGenerator> Apply(IObservable<IMetricGenerator> generators)
    {
        return generators
            .CombineLatest(options)
            .Select(tuple => CreateGenerator(tuple.First, tuple.Second));
    }

    private IMetricGenerator CreateGenerator(IMetricGenerator generator, IActuatorOptions actuatorOptions)
    {
        return new ActuatorMetricGeneratorAdapter(actuatorOptions.Get(factory), generator);
    }
}