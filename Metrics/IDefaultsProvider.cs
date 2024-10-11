namespace IoTDeviceSimulation.Metrics;

public interface IDefaultsProvider<out T>
{
    T Get();
}