using System;
using IoTDeviceSimulation.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public static class ActuatorRegistrar
{
    public static IServiceCollection AddActuator(this IServiceCollection services)
    {
        return services
            .AddSingleton<MetricActuatorOperator>()
            .AddSingleton<IActuatorFactory, ActuatorFactory>()
            .AddSingletonWithImplementedInterface<IObservable<IActuatorOptions>, ActuatorOptionsViewModel>();
    }
}