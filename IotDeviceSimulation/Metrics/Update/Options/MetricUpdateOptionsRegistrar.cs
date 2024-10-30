using System;
using IoTDeviceSimulation.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics.Update.Options;

public static class MetricUpdateOptionsRegistrar
{
    public static IServiceCollection AddMetricUpdateOptions(this IServiceCollection services)
    {
        return services
            .AddSingletonWithImplementedInterface<IObservable<MetricUpdateOptions>, MetricUpdateOptionsViewModel>();
    }
}