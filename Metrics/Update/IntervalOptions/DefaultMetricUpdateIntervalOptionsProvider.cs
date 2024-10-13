namespace IoTDeviceSimulation.Metrics.Update.IntervalOptions;

public class DefaultMetricUpdateIntervalOptionsProvider() 
    : AsIsDefaultsProvider<MetricUpdateIntervalOptions>(new(10));