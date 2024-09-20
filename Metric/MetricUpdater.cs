using System;
using System.Threading;
using System.Threading.Tasks;

namespace IoTDeviceSimulation.Metric;

public class MetricUpdater(MetricViewModel metricViewModel,  MetricUpdaterViewModel updaterViewModel)
{
    public Task StartMetricFieldUpdateAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() => UpdateMetricFieldAsync(cancellationToken), cancellationToken);
    }

    private async Task UpdateMetricFieldAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested) 
        {
            await Task.Delay(
                TimeSpan.FromSeconds(updaterViewModel.DelayBetweenUpdatesInSeconds), cancellationToken);
            
            metricViewModel.Value = Random.Shared.NextDouble();
        }
    }
}