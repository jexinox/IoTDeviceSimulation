using System;
using System.Reactive;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics;

public class MetricViewModel : ReactiveObject, IObserver<Metric>
{
    private readonly Lazy<IObserver<Metric>> _internalObserver;

    private double _value;
    
    public MetricViewModel(IDefaultsProvider<Metric> metricDefaultsProvider)
    {
        _value = metricDefaultsProvider.Get().Value;
        _internalObserver = new(() => Observer.Create<Metric>(metric => Value = metric.Value));
    }

    public double Value
    {
        get => _value;
        private set => _value = this.RaiseAndSetIfChanged(ref _value, value);
    }

    public void OnCompleted() => _internalObserver.Value.OnCompleted();

    public void OnError(Exception error) => _internalObserver.Value.OnError(error);

    public void OnNext(Metric value) => _internalObserver.Value.OnNext(value);
}