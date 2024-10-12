using System;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public static class MetricGeneratorRegistrar
{
    public static IServiceCollection AddMetricGenerators(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IDefaultsProvider<MetricGeneratorOptions>>(_ =>
                new AsIsDefaultsProvider<MetricGeneratorOptions>(new(MetricGeneratorType.Random)))
            .AddSingleton<MetricGeneratorOptionsProvider>()
            .AddSingleton<IMetricGeneratorOptionsProvider>(
                sp => sp.GetRequiredService<MetricGeneratorOptionsProvider>())
            .AddSingleton<IObserver<MetricGeneratorOptions>>(
                sp => sp.GetRequiredService<MetricGeneratorOptionsProvider>())
            .AddSingleton<Random>()
            .AddSingleton<RandomMetricGenerator>()
            .AddSingleton<IMetricGeneratorFactory, MetricGeneratorFactory>()
            .AddSingleton<IMetricGeneratorProvider, MetricGeneratorProvider>()
            .AddSingleton<MetricGeneratorOptionsViewModel>()
            .AddSingleton<IObservable<MetricGeneratorOptions>>(
                sp => sp.GetRequiredService<MetricGeneratorOptionsViewModel>())
            .AddSingleton<ISubscriber, DefaultSubscriber<MetricGeneratorOptions>>();
    }
}