using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

public class ActuatorOptionsViewModel : ReactiveObject, IObservable<IActuatorOptions>
{
    private readonly IObservable<IActuatorOptions> _internalObservable;

    private ActuatorMode _actuatorMode = ActuatorMode.Auto;

    public ActuatorOptionsViewModel(
        AutoActuatorOptionsViewModel autoOptionsViewModel,
        ManualActuatorOptionsViewModel manualOptionsViewModel)
    {
        AutoOptionsViewModel = autoOptionsViewModel;
        ManualOptionsViewModel = manualOptionsViewModel;
        _internalObservable = this
            .WhenAnyValue(x => x.SelectedMode)
            .Select<ActuatorMode, IObservable<IActuatorOptions>>(mode => mode switch
            {
                ActuatorMode.Auto => autoOptionsViewModel,
                ActuatorMode.Manual => Observable.Return(
                    new ManualActuatorOptions(manualOptionsViewModel.ManualHandleCommand)),
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            })
            .Switch();

        this
            .WhenAnyValue(viewModel => viewModel.SelectedMode)
            .Subscribe(_ =>
            {
                this.RaisePropertyChanged(nameof(ShowManualOptionsViewModel));
                this.RaisePropertyChanged(nameof(ShowAutoOptionsViewModel));
            });
    }
    
    public static IEnumerable<ActuatorMode> AvailableModes => Enum.GetValues<ActuatorMode>();

    public bool ShowAutoOptionsViewModel => SelectedMode == ActuatorMode.Auto;
    
    public AutoActuatorOptionsViewModel AutoOptionsViewModel { get; }
    
    public bool ShowManualOptionsViewModel => SelectedMode == ActuatorMode.Manual;
    
    public ManualActuatorOptionsViewModel ManualOptionsViewModel { get; }
    
    public ActuatorMode SelectedMode
    {
        get => _actuatorMode;
        set => this.RaiseAndSetIfChanged(ref _actuatorMode, value);
    }
    
    public IDisposable Subscribe(IObserver<IActuatorOptions> observer) => _internalObservable.Subscribe(observer);
}