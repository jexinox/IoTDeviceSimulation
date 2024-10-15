using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

public class ManualActuatorOptionsViewModel : ReactiveObject, IObservable<ManualActuatorOptions>
{
    private readonly IObservable<ManualActuatorOptions> _internalObservable;
    
    private double _metricChange = 0.2;
    
    public ManualActuatorOptionsViewModel()
    {
        ManualHandleCommand = ReactiveCommand.Create(ManualHandle);
        _internalObservable = Observable.Return(new ManualActuatorOptions(ManualHandleCommand));
    }
    
    public ReactiveCommand<Unit, ManualActuatorHandleEvent> ManualHandleCommand { get; }

    public double MetricChange
    {
        get => _metricChange;
        set => this.RaiseAndSetIfChanged(ref _metricChange, value);
    }
    
    public IDisposable Subscribe(IObserver<ManualActuatorOptions> observer) => _internalObservable.Subscribe(observer);

    private ManualActuatorHandleEvent ManualHandle()
    {
        return new(_metricChange);
    }
}