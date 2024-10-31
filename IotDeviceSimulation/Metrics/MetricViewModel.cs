using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics;

public class MetricViewModel : ReactiveObject, IAsyncObserver<Metric>
{
    private readonly IAsyncObserver<Metric> _internalObserver;

    private Metric _metric = new();
    
    public MetricViewModel()
    {
        _internalObserver = AsyncObserver.Create<Metric>(metric =>
        {
            Metric = metric.Value;
            return ValueTask.CompletedTask;
        });
    }

    public double Metric
    {
        get => _metric.Value;
        private set => _metric = this.RaiseAndSetIfChanged(ref _metric, new(value));
    }

    public ValueTask OnCompletedAsync() => _internalObserver.OnCompletedAsync();

    public ValueTask OnErrorAsync(Exception error) => _internalObserver.OnErrorAsync(error);

    public ValueTask OnNextAsync(Metric value) => _internalObserver.OnNextAsync(value);
}