using System;

namespace IoTDeviceSimulation.Metrics.Update.IntervalOptions;

public record MetricUpdateIntervalOptions
{
    public MetricUpdateIntervalOptions(int secondsBetweenUpdates)
    {
        IntervalBetweenUpdates = TimeSpan.FromSeconds(secondsBetweenUpdates);
    }
    
    public TimeSpan IntervalBetweenUpdates { get; }
}