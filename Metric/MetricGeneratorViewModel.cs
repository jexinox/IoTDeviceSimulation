using ReactiveUI;

namespace IoTDeviceSimulation.Metric;

public class MetricGeneratorViewModel : ReactiveObject
{
    private int _delayBetweenGenerationsInSeconds = 10;

    public int DelayBetweenGenerationsInSeconds
    {
        get => _delayBetweenGenerationsInSeconds;
        set => this.RaiseAndSetIfChanged(ref _delayBetweenGenerationsInSeconds, value);
    }
}