using System;
using System.Reactive.Linq;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Auto;

public class AutoActuatorOptionsViewModel : ReactiveObject, IObservable<AutoActuatorOptions>
{
    private readonly IObservable<AutoActuatorOptions> _internalObservable;
    
    private AutoActuatorOptions options = new();
    
    public AutoActuatorOptionsViewModel()
    {
        _internalObservable = this
            .WhenAnyValue(model => model.MetricValueLimit, model => model.MetricChange)
            .Select(tuple => new AutoActuatorOptions(tuple.Item1, tuple.Item2));
    }

    public double MetricValueLimit
    {
        get => options.MetricValueLimit;
        set => this.RaiseAndSetIfChanged(ref options, options with { MetricValueLimit = value });
    }

    public double MetricChange
    {
        get => options.MetricChange;
        set => this.RaiseAndSetIfChanged(ref options, options with { MetricChange = value });
    }
    
    public IDisposable Subscribe(IObserver<AutoActuatorOptions> observer) => _internalObservable.Subscribe(observer);
}