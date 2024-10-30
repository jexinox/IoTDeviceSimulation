using System;
using System.Reactive.Linq;
using IoTDeviceSimulation.Metrics.Update.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class MetricGenerationOperator(IObservable<MetricUpdateOptions> updateOptions)
{
    public IObservable<Metric> Apply(IObservable<IMetricGenerator> generators)
    {
        return updateOptions
            .Select(options => options.IntervalBetweenUpdates)
            .Select(Observable.Interval)
            .Switch()
            .WithLatestFrom(generators, (_, generator) => generator)
            .Scan(new Metric(), (metric, generator) => generator.Generate(metric));
    }
}