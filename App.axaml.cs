using System.Threading;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using IoTDeviceSimulation.MainWindow;
using IoTDeviceSimulation.Metric;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            .AddSingleton<MetricViewModel>()
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<MainWindowView>()
            .AddOptions()
            .AddSingleton<IOptionsFactory<MetricGeneratorOptions>, MetricGeneratorOptionsFactory>()
            .AddSingleton<MetricGeneratorViewModel>()
            .AddSingleton<MetricGenerator>();
            
        var serviceProvider = serviceCollection.BuildServiceProvider(
            new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true});

        var cts = new CancellationTokenSource();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = serviceProvider.GetRequiredService<MainWindowView>();
            desktop.ShutdownRequested += (_, _) => cts.Cancel();
            desktop.Exit += (_, _) => cts.Cancel();
        }
        
        serviceProvider.GetRequiredService<MetricGenerator>().StartGeneration(cts.Token);

        base.OnFrameworkInitializationCompleted();
    }
}