namespace IoTDeviceSimulation.Metrics.Update.Generation.Options;

public class DefaultMetricGeneratorOptionsProvider() 
    : AsIsDefaultsProvider<MetricGeneratorOptions>(new(MetricGeneratorType.Random));