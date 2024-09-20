using Microsoft.Extensions.Options;

namespace IoTDeviceSimulation.Metric;

public class MetricUpdaterOptionsFactory(MetricUpdaterViewModel updaterViewModel) : IOptionsFactory<MetricUpdaterOptions>
{
    public MetricUpdaterOptions Create(string name) 
        => new(updaterViewModel.DelayBetweenUpdatesInSeconds);
}