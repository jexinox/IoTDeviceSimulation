using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace IoTDeviceSimulation.Metric;

public class MetricUpdater(MetricViewModel metricViewModel, IOptionsMonitor<MetricUpdaterOptions> options)
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
                TimeSpan.FromSeconds(options.CurrentValue.DelayBetweenGenerationsInSeconds), cancellationToken);
            
            metricViewModel.Value = Random.Shared.NextDouble();
        }
    }
}