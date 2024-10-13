using System;
using IoTDeviceSimulation.Extensions;
using IoTDeviceSimulation.Subscriptions;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics.Update.IntervalOptions;

public static class MetricUpdateOptionsRegistrar
{
    public static IServiceCollection AddMetricUpdateOptions(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDefaultsProvider<MetricUpdateIntervalOptions>, DefaultMetricUpdateIntervalOptionsProvider>()
            .AddSingletonWithImplementedInterface<IObservable<MetricUpdateIntervalOptions>, MetricUpdateOptionsViewModel>()
            .AddSingleton<ISubscriber, DefaultSubscriber<MetricUpdateIntervalOptions>>();
    }
}