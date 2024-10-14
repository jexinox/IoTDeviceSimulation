using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Options;

public class MetricGeneratorOptionsViewModel : ReactiveObject, IObservable<MetricGeneratorOptions>
{
    private readonly Lazy<IObservable<MetricGeneratorOptions>> _internalObservable;
    
    private MetricGeneratorType _generatorType = MetricGeneratorType.Random;

    public MetricGeneratorOptionsViewModel()
    {
        _internalObservable = new(() => this
            .WhenAnyValue(vm => vm.SelectedGenerator)
            .Select(generatorType => new MetricGeneratorOptions(generatorType)));
    }
    
    public IEnumerable<MetricGeneratorType> AvailableGenerators { get; } = Enum.GetValues<MetricGeneratorType>();

    public MetricGeneratorType SelectedGenerator
    {
        get => _generatorType;
        set => this.RaiseAndSetIfChanged(ref _generatorType, value);
    }
    
    public IDisposable Subscribe(IObserver<MetricGeneratorOptions> observer) 
        => _internalObservable.Value.Subscribe(observer);
}