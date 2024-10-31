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
            .WhenAnyValue(
                model => model.ClientId,
                model => model.Topic,
                model => model.Host,
                model => model.Port)
            .Throttle(TimeSpan.FromMilliseconds(100))
            .Select(tuple => new MqttActuatorOptions(tuple.Item1, tuple.Item2));
    }

    public string ClientId
    {
        get => options.ClientId;
        set => this.RaiseAndSetIfChanged(ref options, options with { ClientId = value });
    }

    public string Topic
    {
        get => options.Topic;
        set => this.RaiseAndSetIfChanged(ref options, options with { Topic = value });
    }
    
    public string Host
    {
        get => options.Host;
        set => this.RaiseAndSetIfChanged(ref options, options with { Host = value });
    }
    
    public int Port
    {
        get => options.Port;
        set => this.RaiseAndSetIfChanged(ref options, options with { Port = value });
    }
    
    public IDisposable Subscribe(IObserver<MqttActuatorOptions> observer) => _internalObservable.Subscribe(observer);
}