namespace IoTDeviceSimulation.Metrics.Generation;

public interface IMetricGenerator
{ 
    Metric Generate(Metric metric);
}