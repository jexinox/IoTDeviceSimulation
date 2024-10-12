using System;
using IoTDeviceSimulation.Metrics.Update;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics;

public static class MetricViewModelRegistrar
{
    public static IServiceCollection AddMetric(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDefaultsProvider<Metric>>(_ => new AsIsDefaultsProvider<Metric>(new(1)))
            .AddSingleton<MetricViewModel>()
            .AddSingleton<IObserver<Metric>>(sp => sp.GetRequiredService<MetricViewModel>())
            .AddMetricUpdater()
            .AddSingleton<ISubscriber, DefaultSubscriber<Metric>>();
    }
}