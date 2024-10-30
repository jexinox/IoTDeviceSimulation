using System;
using System.Reactive;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics;

public class MetricViewModel : ReactiveObject, IObserver<Metric>
{
    private readonly IObserver<Metric> _internalObserver;

    private Metric _metric = new();
    
    public MetricViewModel()
    {
        _internalObserver = Observer.Create<Metric>(metric => Metric = metric.Value);
    }

    public double Metric
    {
        get => _metric.Value;
        private set => _metric = this.RaiseAndSetIfChanged(ref _metric, new(value));
    }

    public void OnCompleted() => _internalObserver.OnCompleted();

    public void OnError(Exception error) => _internalObserver.OnError(error);

    public void OnNext(Metric value) => _internalObserver.OnNext(value);
}