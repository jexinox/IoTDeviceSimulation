using System;
using IoTDeviceSimulation.Extensions;
using IoTDeviceSimulation.Metrics.Update.Generation;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics.Update.Options;

public static class MetricUpdateOptionsRegistrar
{
    public static IServiceCollection AddMetricUpdateOptions(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDefaultsProvider<MetricUpdateOptions>>(
                _ => new AsIsDefaultsProvider<MetricUpdateOptions>(new(2)))
            .AddSingletonWithImplementedInterface<IObservable<MetricUpdateOptions>, MetricUpdateOptionsViewModel>()
            .AddSingletonWithImplementedInterfaces<
                IMetricUpdateOptionsProvider, IObserver<MetricUpdateOptions>, MetricUpdateOptionsProvider>()
            .AddSingleton<ISubscriber, DefaultSubscriber<MetricUpdateOptions>>();
    }
}