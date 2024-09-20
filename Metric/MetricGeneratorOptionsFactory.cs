using Microsoft.Extensions.Options;

namespace IoTDeviceSimulation.Metric;

public class MetricGeneratorOptionsFactory(MetricGeneratorViewModel generatorViewModel) : IOptionsFactory<MetricGeneratorOptions>
{
    public MetricGeneratorOptions Create(string name) 
        => new(generatorViewModel.DelayBetweenGenerationsInSeconds);
}