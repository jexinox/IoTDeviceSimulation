using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using IoTDeviceSimulation.Metrics.Update.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class MetricGenerationOperator(IObservable<MetricUpdateOptions> updateOptions)
{
    public IAsyncObservable<Metric> Apply(IAsyncObservable<IMetricGenerator> generators)
    {
        return updateOptions
            .ToAsyncObservable()
            .Select(options => options.IntervalBetweenUpdates)
            .Select(AsyncObservable.Interval)
            .Switch()
            .WithLatestFrom(generators, (_, generator) => generator)
            .Scan(new Metric(), Accumulator);
    }

    private Metric Accumulator(Metric metric, IMetricGenerator generator)
    {
        return generator.Generate(metric);
    }
}