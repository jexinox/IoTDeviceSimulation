using ReactiveUI;

namespace IoTDeviceSimulation.Metric;

public class MetricViewModel : ReactiveObject
{
    private double _value;

    public double Value
    {
        get => _value;
        set => this.RaiseAndSetIfChanged(ref _value, value);
    }
}