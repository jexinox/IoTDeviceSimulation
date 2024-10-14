using System;
using IoTDeviceSimulation.Extensions;
using IoTDeviceSimulation.Metrics.Update.Generation.Options;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public static class MetricGeneratorRegistrar
{
    public static IServiceCollection AddMetricGenerators(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<MetricGeneratorOperator>()
            .AddSingletonWithImplementedInterface<IObservable<MetricGeneratorOptions>, MetricGeneratorOptionsViewModel>();
    }
}