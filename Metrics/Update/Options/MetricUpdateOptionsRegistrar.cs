using System;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics.Update.Options;

public static class MetricUpdateOptionsRegistrar
{
    public static IServiceCollection AddMetricUpdateOptions(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDefaultsProvider<MetricUpdateOptions>>(
                _ => new DefaultsProvider<MetricUpdateOptions>(new(2)))
            .AddSingleton<MetricUpdateOptionsViewModel>()
            .AddSingleton<IObservable<MetricUpdateOptions>>(sp => sp.GetRequiredService<MetricUpdateOptionsViewModel>())
            .AddSingleton<MetricUpdateOptionsProvider>()
            .AddSingleton<IMetricUpdateOptionsProvider>(sp => sp.GetRequiredService<MetricUpdateOptionsProvider>())
            .AddSingleton<IObserver<MetricUpdateOptions>>(sp => sp.GetRequiredService<MetricUpdateOptionsProvider>())
            .AddSingleton<ISubscriber, DefaultSubscriber<MetricUpdateOptions>>();
    }
}