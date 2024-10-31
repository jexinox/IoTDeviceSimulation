using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Auto;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Manual;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Mqtt;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

public class ActuatorOptionsViewModel : ReactiveObject, IObservable<IActuatorOptions>
{
    private readonly IObservable<IActuatorOptions> _internalObservable;

    private ActuatorMode _actuatorMode = ActuatorMode.Auto;

    public ActuatorOptionsViewModel(
        AutoActuatorOptionsViewModel autoOptionsViewModel,
        ManualActuatorOptionsViewModel manualOptionsViewModel,
        MqttActuatorOptionsViewModel mqttOptionsViewModel)
    {
        AutoOptionsViewModel = autoOptionsViewModel;
        ManualOptionsViewModel = manualOptionsViewModel;
        MqttOptionsViewModel = mqttOptionsViewModel;

        _internalObservable = this
            .WhenAnyValue(x => x.SelectedMode)
            .Select(MapActuatorModeToObservable)
            .Switch();

        this
            .WhenAnyValue(viewModel => viewModel.SelectedMode)
            .Subscribe(_ =>
            {
                this.RaisePropertyChanged(nameof(ShowManualOptionsViewModel));
                this.RaisePropertyChanged(nameof(ShowAutoOptionsViewModel));
                this.RaisePropertyChanged(nameof(ShowMqttOptionsViewModel));
            });
    }
    
    public static IEnumerable<ActuatorMode> AvailableModes => Enum.GetValues<ActuatorMode>();

    public bool ShowAutoOptionsViewModel => SelectedMode == ActuatorMode.Auto;
    
    public AutoActuatorOptionsViewModel AutoOptionsViewModel { get; }
    
    public bool ShowManualOptionsViewModel => SelectedMode == ActuatorMode.Manual;
    
    public ManualActuatorOptionsViewModel ManualOptionsViewModel { get; }
    
    public bool ShowMqttOptionsViewModel => SelectedMode == ActuatorMode.Mqtt;
    
    public MqttActuatorOptionsViewModel MqttOptionsViewModel { get; }
    
    public ActuatorMode SelectedMode
    {
        get => _actuatorMode;
        set => this.RaiseAndSetIfChanged(ref _actuatorMode, value);
    }
    
    public IDisposable Subscribe(IObserver<IActuatorOptions> observer) => _internalObservable.Subscribe(observer);
    
    private IObservable<IActuatorOptions> MapActuatorModeToObservable(ActuatorMode mode)
    {
        return mode switch
        {
            ActuatorMode.Auto => AutoOptionsViewModel,
            ActuatorMode.Manual => ManualOptionsViewModel,
            ActuatorMode.Mqtt => MqttOptionsViewModel,
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
    }
}