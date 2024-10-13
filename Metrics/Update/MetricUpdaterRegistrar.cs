using System;
using IoTDeviceSimulation.Metrics.Update.IntervalOptions;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics.Update;

public static class MetricUpdaterRegistrar
{
    public static IServiceCollection AddMetricUpdater(this IServiceCollection services)
    {
        return services
            .AddSingleton<IObservable<Metric>, MetricUpdater>()
            .AddMetricUpdateOptions();
    }
}