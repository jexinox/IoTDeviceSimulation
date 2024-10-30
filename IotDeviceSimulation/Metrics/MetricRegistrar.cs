using System;
using IoTDeviceSimulation.Extensions;
using IoTDeviceSimulation.Metrics.Update.Generation;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator;
using IoTDeviceSimulation.Metrics.Update.Options;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics;

public static class MetricRegistrar
{
    public static IServiceCollection AddMetric(this IServiceCollection services)
    {
        return services
            .AddSingletonWithImplementedInterface<IObserver<Metric>, MetricViewModel>()
            .AddMetricGenerators()
            .AddActuator()
            .AddMetricUpdateOptions()
            .AddSingleton<MainScenario>();
    }
}