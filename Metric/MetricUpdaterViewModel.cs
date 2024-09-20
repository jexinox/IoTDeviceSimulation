using ReactiveUI;

namespace IoTDeviceSimulation.Metric;

public class MetricUpdaterViewModel : ReactiveObject
{
    private int _delayBetweenUpdatesInSeconds = 10;

    public int DelayBetweenUpdatesInSeconds
    {
        get => _delayBetweenUpdatesInSeconds;
        set => this.RaiseAndSetIfChanged(ref _delayBetweenUpdatesInSeconds, value);
    }
}