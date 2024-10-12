using System;
using IoTDeviceSimulation.Metrics.Update;
using IoTDeviceSimulation.Metrics.Update.Generation;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics;

public static class MetricRegistrar
{
    public static IServiceCollection AddMetric(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDefaultsProvider<Metric>>(_ => new AsIsDefaultsProvider<Metric>(new(1)))
            .AddSingleton<MetricViewModel>()
            .AddSingleton<IObserver<Metric>>(sp => sp.GetRequiredService<MetricViewModel>())
            .AddMetricUpdater()
            .AddMetricGenerators()
            .AddSingleton<ISubscriber, DefaultSubscriber<Metric>>();
    }
}