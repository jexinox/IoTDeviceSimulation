using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSingletonWithImplementedInterface<TInterface, TImplementation>(
        this IServiceCollection services)
        where TImplementation : class, TInterface 
        where TInterface : class 
    {
        return services
            .AddSingleton<TImplementation>()
            .AddSingleton<TInterface>(sp => sp.GetRequiredService<TImplementation>());
    }
    
    public static IServiceCollection AddSingletonWithImplementedInterfaces<TInterface1, TInterface2, TImplementation>(
        this IServiceCollection services) 
        where TImplementation : class, TInterface1, TInterface2 
        where TInterface1 : class 
        where TInterface2 : class
    {
        return services
            .AddSingleton<TImplementation>()
            .AddSingleton<TInterface1>(sp => sp.GetRequiredService<TImplementation>())
            .AddSingleton<TInterface2>(sp => sp.GetRequiredService<TImplementation>());
    }
}