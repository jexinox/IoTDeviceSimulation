using System;
using System.Reactive.Linq;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Mqtt;

public class MqttActuatorOptionsViewModel : ReactiveObject, IObservable<MqttActuatorOptions>
{
    private readonly IObservable<MqttActuatorOptions> _internalObservable;
    
    private MqttActuatorOptions options = new();
    
    public MqttActuatorOptionsViewModel()
    {
        _internalObservable = this
            .WhenAnyValue(model => model.Topic)
            .Select(topic => new MqttActuatorOptions(topic));
    }
    
    public string Topic
    {
        get => options.Topic;
        set => this.RaiseAndSetIfChanged(ref options, new(Topic: value));
    }
    
    public IDisposable Subscribe(IObserver<MqttActuatorOptions> observer) => _internalObservable.Subscribe(observer);
}