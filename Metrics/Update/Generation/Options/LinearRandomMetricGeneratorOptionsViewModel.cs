using System;
using System.Reactive.Linq;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Options;

public class LinearRandomMetricGeneratorOptionsViewModel : ReactiveObject, IObservable<LinearRandomMetricGeneratorOptions>
{
    private readonly IObservable<LinearRandomMetricGeneratorOptions> _internalObservable;
    
    private LinearRandomMetricGeneratorOptions _options = new();

    public LinearRandomMetricGeneratorOptionsViewModel()
    {
        _internalObservable = this
            .WhenAnyValue(viewModel => viewModel.MinIncrement, viewModel => viewModel.MaxIncrement)
            .Select(tuple => new LinearRandomMetricGeneratorOptions(tuple.Item1, tuple.Item2));
    }

    public double MaxIncrement
    {
        get => _options.MaxIncrement;
        set => this.RaiseAndSetIfChanged(ref _options, _options with { MaxIncrement = value });
    }
    
    public double MinIncrement
    {
        get => _options.MinIncrement;
        set => this.RaiseAndSetIfChanged(ref _options, _options with { MinIncrement = value });
    }
    
    public IDisposable Subscribe(IObserver<LinearRandomMetricGeneratorOptions> observer) 
        => _internalObservable.Subscribe(observer);
}