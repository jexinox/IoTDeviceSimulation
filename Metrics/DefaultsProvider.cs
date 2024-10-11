namespace IoTDeviceSimulation.Metrics;

public class DefaultsProvider<T>(T defaultValue) : IDefaultsProvider<T>
{
    public T Get() => defaultValue;
}