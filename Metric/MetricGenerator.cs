using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace IoTDeviceSimulation.Metric;

public class MetricGenerator(MetricViewModel metricViewModel, IOptionsMonitor<MetricGeneratorOptions> options)
{
    public Task StartGeneration(CancellationToken cancellationToken)
    {
        return Task.Run(() => GenerationLoop(cancellationToken), cancellationToken);
    }

    private async Task GenerationLoop(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested) 
        {
            await Task.Delay(
                TimeSpan.FromSeconds(options.CurrentValue.DelayBetweenGenerationsInSeconds), cancellationToken);
            metricViewModel.Value = Random.Shared.NextDouble();
        }
    }
}