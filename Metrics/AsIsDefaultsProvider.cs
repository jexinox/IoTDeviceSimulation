namespace IoTDeviceSimulation.Metrics;

public class AsIsDefaultsProvider<T>(T defaultValue) : IDefaultsProvider<T>
{
    public T Get() => defaultValue;
}