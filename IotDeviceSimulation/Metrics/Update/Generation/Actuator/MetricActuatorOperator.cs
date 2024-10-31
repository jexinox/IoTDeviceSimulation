using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public class MetricActuatorOperator(IObservable<IActuatorOptions> options, IActuatorFactory factory)
{
    public IAsyncObservable<IMetricGenerator> Apply(IObservable<IMetricGenerator> generators)
    {
        return generators
            .ToAsyncObservable()
            .CombineLatest(options.ToAsyncObservable())
            .Select(async tuple => await CreateGenerator(tuple.Item1, tuple.Item2));
    }

    private async ValueTask<IMetricGenerator> CreateGenerator(IMetricGenerator generator, IActuatorOptions actuatorOptions)
    {
        return new ActuatorMetricGeneratorAdapter(await actuatorOptions.Get(factory), generator);
    }
}