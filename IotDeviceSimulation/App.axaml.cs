using System.Threading;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using IoTDeviceSimulation.MainWindow;
using IoTDeviceSimulation.Metrics;
using Microsoft.Extensions.DependencyInjection;

namespace IoTDeviceSimulation;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection
            .AddSingleton<MainWindowView>()
            .AddSingleton<CancellationTokenSource>()
            .AddMetric()
            .AddSingleton<MainWindowViewModel>();
            
        var serviceProvider = serviceCollection.BuildServiceProvider(
            new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true});
        
        var cts = serviceProvider.GetRequiredService<CancellationTokenSource>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = serviceProvider.GetRequiredService<MainWindowView>();
            desktop.ShutdownRequested += (_, _) => cts.Cancel();
            desktop.Exit += (_, _) => cts.Cancel();
        }

        serviceProvider.GetRequiredService<MainScenario>().Run();
        
        base.OnFrameworkInitializationCompleted();
    }
}