namespace IoTDeviceSimulation.Metrics.Update.Generation;

public interface IMetricGenerator
{ 
    Metric Generate(Metric metric);
}