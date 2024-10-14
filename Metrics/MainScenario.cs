using System;
using System.Reactive.Linq;
using IoTDeviceSimulation.Metrics.Update.Generation;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator;
using IoTDeviceSimulation.Metrics.Update.Generation.Options;
using IoTDeviceSimulation.Metrics.Update.Options;

namespace IoTDeviceSimulation.Metrics;

public class MainScenario(
    IObserver<Metric> metricObserver,
    IObservable<MetricUpdateOptions> metricUpdateOptionsViewModel,
    IObservable<MetricGeneratorOptions> generatorOptionsViewModel,
    MetricGeneratorOperator metricGeneratorOperator,
    MetricActuatorOperator metricActuatorOperator)
{
    public void Run()
    {
        var metricGenerators = metricGeneratorOperator.Apply(generatorOptionsViewModel);
        
        metricUpdateOptionsViewModel
            .Select(options => options.IntervalBetweenUpdates)
            .Select(Observable.Interval)
            .Switch()
            .WithLatestFrom(metricGenerators)
            .Select(tuple => tuple.Second)
            .Scan(new Metric(), (metric, generator) => generator.Generate(metric))
            .Subscribe(metricObserver);
    }
}