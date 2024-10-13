using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public static class ActuatorRegistrar
{
    public static IServiceCollection AddActuator(this IServiceCollection services)
    {
        return services
            .Decorate<IMetricGeneratorFactory, ActuatorMetricGeneratorFactoryDecorator>()
            .AddSingleton<IActuatorOptionsProvider, StubProvider>();
    }
    
    private class StubProvider : IActuatorOptionsProvider
    {
        public IActuatorOptions Get()
        {
            return new AutoActuatorOptions(0.7, 0.2);
        }
    }
}