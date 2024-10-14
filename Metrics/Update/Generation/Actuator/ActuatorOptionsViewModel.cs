using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public class ActuatorOptionsViewModel : ReactiveObject, IObservable<IActuatorOptions>
{
    private readonly Lazy<IObservable<IActuatorOptions>> _internalObservable;

    private ActuatorMode _actuatorMode = ActuatorMode.Auto;

    public ActuatorOptionsViewModel()
    {
        _internalObservable = new(() => this
            .WhenAnyValue(vm => vm.SelectedMode)
            .Select<ActuatorMode, IActuatorOptions>(mode =>
                mode switch
                {
                    ActuatorMode.Auto => new AutoActuatorOptions(0.7, 0.2),
                    ActuatorMode.Manual => new ManualActuatorOptions(),
                    _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
                }));
    }
    
    public static IEnumerable<ActuatorMode> AvailableModes => Enum.GetValues<ActuatorMode>();

    public ActuatorMode SelectedMode
    {
        get => _actuatorMode;
        set => this.RaiseAndSetIfChanged(ref _actuatorMode, value);
    }
    
    public IDisposable Subscribe(IObserver<IActuatorOptions> observer) => _internalObservable.Value.Subscribe(observer);
}