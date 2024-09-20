using IoTDeviceSimulation.Metric;
using ReactiveUI;

namespace IoTDeviceSimulation;

public class MainWindowViewModel(
    MetricViewModel metricViewModel,
    MetricGeneratorViewModel metricGeneratorViewModel) : ReactiveObject
{
    public MetricViewModel MetricViewModel { get; } = metricViewModel;

    public MetricGeneratorViewModel MetricGeneratorViewModel { get; } = metricGeneratorViewModel;
}