using IoTDeviceSimulation.Metric;
using ReactiveUI;

namespace IoTDeviceSimulation.MainWindow;

public class MainWindowViewModel(
    MetricViewModel metricViewModel,
    MetricGeneratorViewModel metricGeneratorViewModel) : ReactiveObject
{
    public MetricViewModel MetricViewModel { get; } = metricViewModel;

    public MetricGeneratorViewModel MetricGeneratorViewModel { get; } = metricGeneratorViewModel;
}