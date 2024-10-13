using IoTDeviceSimulation.Metrics.Update.Generation;
using IoTDeviceSimulation.Metrics.Update.IntervalOptions;

namespace IoTDeviceSimulation.Metrics.Update;

public record MetricUpdateOptions(IMetricGenerator Generator, MetricUpdateIntervalOptions IntervalOptions);