using System;
using IoTDeviceSimulation.Extensions;
using IoTDeviceSimulation.Metrics.Update;
using IoTDeviceSimulation.Metrics.Update.Generation;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics;

public static class MetricRegistrar
{
    public static IServiceCollection AddMetric(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDefaultsProvider<Metric>>(_ => new AsIsDefaultsProvider<Metric>(new(1)))
            .AddSingletonWithImplementedInterface<IObserver<Metric>, MetricViewModel>()
            .AddMetricUpdater()
            .AddMetricGenerators()
            .AddActuator()
            .AddSingleton<ISubscriber, DefaultSubscriber<Metric>>();
    }
}