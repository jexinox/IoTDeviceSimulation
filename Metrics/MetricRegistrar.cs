using System;
using IoTDeviceSimulation.Extensions;
using IoTDeviceSimulation.Metrics.Update;
using IoTDeviceSimulation.Metrics.Update.Generation;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator;
using IoTDeviceSimulation.Subscriptions;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics;

public static class MetricRegistrar
{
    public static IServiceCollection AddMetric(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDefaultsProvider<Metric>, DefaultMetricProvider>()
            .AddSingletonWithImplementedInterface<IObserver<Metric>, MetricViewModel>()
            .AddMetricUpdater()
            .AddMetricGenerators()
            .AddActuator()
            .AddSingleton<ISubscriber, DefaultSubscriber<Metric>>();
    }
}