using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using IoTDeviceSimulation.Metrics.Update.Generation;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

namespace IoTDeviceSimulation.Metrics;

public class MainScenario(
    IAsyncObserver<Metric> metricObserver,
    MetricGenerationOperator metricGenerationOperator,
    MetricGeneratorsProvider metricGeneratorsProvider,
    MetricActuatorOperator metricActuatorOperator,
    MetricValueLimiterOperator metricValueLimiterOperator)
{
    public async Task Run()
    {
        var metricGenerators = metricGeneratorsProvider.Get();
        var actuatedMetricsGenerators = metricActuatorOperator.Apply(metricGenerators);
        var limitedMetricValueGenerators = metricValueLimiterOperator.Apply(actuatedMetricsGenerators);
        var mainMetricStream = metricGenerationOperator.Apply(limitedMetricValueGenerators);
        
        await mainMetricStream.SubscribeAsync(metricObserver);
    }
}