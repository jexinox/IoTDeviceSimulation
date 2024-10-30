using System;
using System.Reactive.Linq;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Options;

public class LinearMetricGeneratorOptionsViewModel : ReactiveObject, IObservable<LinearMetricGeneratorOptions>
{
    private readonly IObservable<LinearMetricGeneratorOptions> _internalObservable;
    
    private LinearMetricGeneratorOptions _options = new();

    public LinearMetricGeneratorOptionsViewModel()
    {
        _internalObservable = this
            .WhenAnyValue(viewModel => viewModel.Increment)
            .Select(increment => new LinearMetricGeneratorOptions(increment));
    }
    
    public double Increment
    {
        get => _options.Increment;
        set => this.RaiseAndSetIfChanged(ref _options, new(value));
    }
    
    public IDisposable Subscribe(IObserver<LinearMetricGeneratorOptions> observer) => _internalObservable.Subscribe(observer);
}