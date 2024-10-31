using Avalonia;
using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace IoTDeviceSimulation;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        RxApp.DefaultExceptionHandler = Observer.Create<Exception>(Console.WriteLine);
        TaskScheduler.UnobservedTaskException += (sender, eventArgs) => Console.WriteLine(eventArgs.Exception); 
        try
        {
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    private static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .UseReactiveUI()
            .LogToTrace();
}