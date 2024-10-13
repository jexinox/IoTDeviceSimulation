using System;
using IoTDeviceSimulation.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Options;

public static class MetricGeneratorOptionsRegistrar
{
    public static IServiceCollection AddMetricGeneratorOptions(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDefaultsProvider<MetricGeneratorOptions>, DefaultMetricGeneratorOptionsProvider>()
            .AddSingletonWithImplementedInterfaces<
                IMetricGeneratorOptionsProvider, IObserver<MetricGeneratorOptions>, MetricGeneratorOptionsProvider>()
            .AddSingletonWithImplementedInterface<
                IObservable<MetricGeneratorOptions>, MetricGeneratorOptionsViewModel>();
    }
}