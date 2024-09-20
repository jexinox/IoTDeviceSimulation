using IoTDeviceSimulation.Metric;
using ReactiveUI;

namespace IoTDeviceSimulation.MainWindow;

public class MainWindowViewModel(
    MetricViewModel metricViewModel,
    MetricUpdaterViewModel metricUpdaterViewModel) : ReactiveObject
{
    public MetricViewModel MetricViewModel { get; } = metricViewModel;

    public MetricUpdaterViewModel MetricUpdaterViewModel { get; } = metricUpdaterViewModel;
}