using System;

namespace IoTDeviceSimulation.Metrics;

public record MetricUpdateOptions
{
    public MetricUpdateOptions(int secondsBetweenUpdates)
    {
        IntervalBetweenUpdates = TimeSpan.FromSeconds(secondsBetweenUpdates);
    }
    
    public TimeSpan IntervalBetweenUpdates { get; }
}