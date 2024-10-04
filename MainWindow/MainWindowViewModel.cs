using IoTDeviceSimulation.Metrics;
using IoTDeviceSimulation.Metrics.Update;
using IoTDeviceSimulation.Metrics.Update.Options;
using ReactiveUI;

namespace IoTDeviceSimulation.MainWindow;

public class MainWindowViewModel(
    MetricViewModel metricViewModel,
    MetricUpdateOptionsViewModel metricUpdateOptionsViewModel) : ReactiveObject
{
    public MetricViewModel MetricViewModel { get; } = metricViewModel;

    public MetricUpdateOptionsViewModel MetricUpdateOptionsViewModel { get; } = metricUpdateOptionsViewModel;
}