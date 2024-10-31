using System;
using System.Threading.Tasks;
using IoTDeviceSimulation.Extensions;
using IoTDeviceSimulation.Metrics.Publishing;
using IoTDeviceSimulation.Metrics.Update.Generation;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator;
using IoTDeviceSimulation.Metrics.Update.Options;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics;

public static class MetricRegistrar
{
    public static IServiceCollection AddMetric(this IServiceCollection services)
    {
        return services
            .AddSingletonWithImplementedInterface<IAsyncObserver<Metric>, MetricViewModel>()
            .AddMetricGenerators()
            .AddActuator()
            .AddMetricUpdateOptions()
            .AddSingleton(new MqttMetricPublisher(new()))
            .AddSingleton<MetricPublishingOperator>()
            .AddSingleton<MainScenario>();
    }
}