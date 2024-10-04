using System;
using System.Threading;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using IoTDeviceSimulation.MainWindow;
using IoTDeviceSimulation.Metrics;
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
            .AddSingleton<MainWindowView>()
            .AddSingleton<CancellationTokenSource>()
            
            .AddSingleton<MetricUpdateOptionsViewModel>()
            .AddSingleton<IObservable<MetricUpdateOptions>>(
                sp => sp.GetRequiredService<MetricUpdateOptionsViewModel>())
            
            .AddSingleton<MetricUpdateOptionsObserverAdapter>()
            .AddSingleton<IOptions<MetricUpdateOptions>>(
                sp => sp.GetRequiredService<MetricUpdateOptionsObserverAdapter>())
            .AddSingleton<IObserver<MetricUpdateOptions>>(
                sp => sp.GetRequiredService<MetricUpdateOptionsObserverAdapter>())
            
            .AddSingleton<MetricViewModel>()
            .AddSingleton<IObserver<Metric>>(sp => sp.GetRequiredService<MetricViewModel>())
            .AddSingleton<IObservable<Metric>, MetricUpdater>()
            
            .AddSingleton<ISubscriber, DefaultSubscriber<MetricUpdateOptions>>()
            .AddSingleton<ISubscriber, DefaultSubscriber<Metric>>()
            
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

        foreach (var subscriber in serviceProvider.GetServices<ISubscriber>())
        {
            subscriber.Subscribe();
        }
        
        base.OnFrameworkInitializationCompleted();
    }
}