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
    MetricActuatorOperator metricActuatorOperator,
    MetricValueLimiterOperator metricValueLimiterOperator)
{
    public void Run()
    {
        var metricGenerators = metricGeneratorOperator.Apply(generatorOptionsViewModel);
        var actuatedMetricsGenerators = metricActuatorOperator.Apply(metricGenerators);
        var limitedMetricValue = metricValueLimiterOperator.Apply(actuatedMetricsGenerators);
        
        var mainMetricStream = metricUpdateOptionsViewModel
            .Select(options => options.IntervalBetweenUpdates)
            .Select(Observable.Interval)
            .Switch()
            .WithLatestFrom(actuatedMetricsGenerators)
            .Select(tuple => tuple.Second)
            .Scan(new Metric(), (metric, generator) => generator.Generate(metric));
        
        mainMetricStream.Subscribe(metricObserver);
    }
}