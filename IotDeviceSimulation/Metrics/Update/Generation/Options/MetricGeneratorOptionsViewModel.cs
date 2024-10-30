using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Options;

public class MetricGeneratorOptionsViewModel : ReactiveObject, IObservable<IMetricGeneratorOptions>
{
    private readonly IObservable<IMetricGeneratorOptions> _internalObservable;
    
    private MetricGeneratorType _generatorType = MetricGeneratorType.Linear;

    public MetricGeneratorOptionsViewModel(
        LinearMetricGeneratorOptionsViewModel linearGeneratorOptionsViewModel,
        LinearRandomMetricGeneratorOptionsViewModel linearRandomGeneratorOptionsViewModel)
    {
        LinearGeneratorOptionsViewModel = linearGeneratorOptionsViewModel;
        LinearRandomGeneratorOptionsViewModel = linearRandomGeneratorOptionsViewModel;
        _internalObservable = this
            .WhenAnyValue(viewModel => viewModel.SelectedGenerator)
            .Select(MapGeneratorTypeToOptions)
            .Switch();

        this.WhenAnyValue(viewModel => viewModel.SelectedGenerator)
            .Subscribe(type =>
            {
                this.RaisePropertyChanged(nameof(ShowLinearGeneratorViewModel));
                this.RaisePropertyChanged(nameof(ShowLinearRandomGeneratorViewModel));
            });
    }
    
    public IEnumerable<MetricGeneratorType> AvailableGenerators { get; } = Enum.GetValues<MetricGeneratorType>();
    
    public MetricGeneratorType SelectedGenerator
    {
        get => _generatorType;
        set => this.RaiseAndSetIfChanged(ref _generatorType, value);
    }
    
    public bool ShowLinearGeneratorViewModel => SelectedGenerator == MetricGeneratorType.Linear;
    
    public LinearMetricGeneratorOptionsViewModel LinearGeneratorOptionsViewModel { get; }
    
    public bool ShowLinearRandomGeneratorViewModel => SelectedGenerator == MetricGeneratorType.LinearRandom;
    
    public LinearRandomMetricGeneratorOptionsViewModel LinearRandomGeneratorOptionsViewModel { get; }
    
    public IDisposable Subscribe(IObserver<IMetricGeneratorOptions> observer) 
        => _internalObservable.Subscribe(observer);

    private IObservable<IMetricGeneratorOptions> MapGeneratorTypeToOptions(MetricGeneratorType generatorType)
    {
        return generatorType switch
        {
            MetricGeneratorType.Linear => LinearGeneratorOptionsViewModel,
            MetricGeneratorType.LinearRandom => LinearRandomGeneratorOptionsViewModel,
            _ => throw new ArgumentOutOfRangeException(nameof(generatorType), generatorType, null)
        };
    }
}