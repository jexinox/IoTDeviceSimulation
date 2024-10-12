using IoTDeviceSimulation.Metrics;
using IoTDeviceSimulation.Metrics.Update.Generation;
using IoTDeviceSimulation.Metrics.Update.Options;
using ReactiveUI;

namespace IoTDeviceSimulation.MainWindow;

public class MainWindowViewModel(
    MetricViewModel metricViewModel,
    MetricUpdateOptionsViewModel metricUpdateOptionsViewModel,
    MetricGeneratorOptionsViewModel metricGeneratorOptionsViewModel) : ReactiveObject
{
    public MetricViewModel MetricViewModel { get; } = metricViewModel;

    public MetricUpdateOptionsViewModel MetricUpdateOptionsViewModel { get; } = metricUpdateOptionsViewModel;
    
    public MetricGeneratorOptionsViewModel MetricGeneratorOptionsViewModel { get; } = metricGeneratorOptionsViewModel;
}