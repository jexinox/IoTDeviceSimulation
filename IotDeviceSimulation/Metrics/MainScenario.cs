using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using IoTDeviceSimulation.Metrics.Publishing;
using IoTDeviceSimulation.Metrics.Update.Generation;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

namespace IoTDeviceSimulation.Metrics;

public class MainScenario(
    IAsyncObserver<Metric> metricObserver,
    MetricGenerationOperator metricGenerationOperator,
    MetricGeneratorsProvider metricGeneratorsProvider,
    MetricActuatorOperator metricActuatorOperator,
    MetricValueLimiterOperator metricValueLimiterOperator,
    MetricPublishingOperator publisherOperator)
{
    public async Task Run()
    {
        var metricGenerators = metricGeneratorsProvider.Get();
        var actuatedMetricsGenerators = metricActuatorOperator.Apply(metricGenerators);
        var limitedMetricValueGenerators = metricValueLimiterOperator.Apply(actuatedMetricsGenerators);
        var metricStream = metricGenerationOperator.Apply(limitedMetricValueGenerators);
        var metricPublishingOperator = publisherOperator.Apply(metricStream);
        
        await metricPublishingOperator.SubscribeAsync(metricObserver);
    }
}