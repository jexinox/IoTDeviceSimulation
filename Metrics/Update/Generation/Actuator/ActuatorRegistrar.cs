using System;
using IoTDeviceSimulation.Extensions;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public static class ActuatorRegistrar
{
    public static IServiceCollection AddActuator(this IServiceCollection services)
    {
        return services
            .AddSingleton<MetricActuatorOperator>()
            .AddSingleton<IActuatorFactory, ActuatorFactory>()
            .AddSingletonWithImplementedInterface<IObservable<IActuatorOptions>, ActuatorOptionsViewModel>()
            .AddSingletonWithImplementedInterface<IObservable<AutoActuatorOptions>, AutoActuatorOptionsViewModel>()
            .AddSingletonWithImplementedInterface<IObservable<ManualActuatorOptions>, ManualActuatorOptionsViewModel>();
    }
}