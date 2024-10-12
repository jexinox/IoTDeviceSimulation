namespace IoTDeviceSimulation.Metrics.Update.Generation;

public interface IMetricGenerator
{ 
    MetricGeneratorType Type { get; }
    
    Metric Generate(Metric metric);
}