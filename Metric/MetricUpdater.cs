using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace IoTDeviceSimulation.Metric;

public class MetricUpdater(MetricViewModel metricViewModel, IOptionsMonitor<MetricUpdaterOptions> options)
{
    public Task StartUpdate(CancellationToken cancellationToken)
    {
        return Task.Run(() => Update(cancellationToken), cancellationToken);
    }

    private async Task Update(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested) 
        {
            await Task.Delay(
                TimeSpan.FromSeconds(options.CurrentValue.DelayBetweenGenerationsInSeconds), cancellationToken);
            
            metricViewModel.Value = Random.Shared.NextDouble();
        }
    }
}