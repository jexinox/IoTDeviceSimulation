using System;
using System.Reactive.Linq;
using IoTDeviceSimulation.Metrics.Update.Generation;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator;
using IoTDeviceSimulation.Metrics.Update.Generation.Options;
using IoTDeviceSimulation.Metrics.Update.Options;

namespace IoTDeviceSimulation.Metrics;

public class MainScenario(
    IObserver<Metric> metricObserver,
    MetricGenerationOperator metricGenerationOperator,
    MetricGeneratorsProvider metricGeneratorsProvider,
    MetricActuatorOperator metricActuatorOperator,
    MetricValueLimiterOperator metricValueLimiterOperator)
{
    public void Run()
    {
        var metricGenerators = metricGeneratorsProvider.Get();
        var actuatedMetricsGenerators = metricActuatorOperator.Apply(metricGenerators);
        var limitedMetricValueGenerators = metricValueLimiterOperator.Apply(actuatedMetricsGenerators);
        var mainMetricStream = metricGenerationOperator.Apply(limitedMetricValueGenerators);
        
        mainMetricStream.Subscribe(metricObserver);
    }
}