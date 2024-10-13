using System;
using IoTDeviceSimulation.Extensions;
using IoTDeviceSimulation.Metrics.Update.Generation.Options;
using IoTDeviceSimulation.Subscriptions;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public static class MetricGeneratorRegistrar
{
    public static IServiceCollection AddMetricGenerators(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<Random>()
            .AddSingleton<IMetricGenerator, RandomMetricGenerator>()
            .AddSingleton<IMetricGeneratorFactory, MetricGeneratorFactory>()
            .AddSingleton<IMetricGeneratorProvider, MetricGeneratorProvider>()
            .AddMetricGeneratorOptions()
            .AddSingleton<ISubscriber, DefaultSubscriber<MetricGeneratorOptions>>();
    }
}