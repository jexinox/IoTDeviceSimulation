using System;
using IoTDeviceSimulation.Metrics.Update.Generation;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

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