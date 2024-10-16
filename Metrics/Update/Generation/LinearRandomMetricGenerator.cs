using System;
using IoTDeviceSimulation.Metrics.Update.Generation.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class LinearRandomMetricGenerator(Random random, LinearRandomMetricGeneratorOptions options) : IMetricGenerator
{
    public Metric Generate(Metric metric)
    {
        var randomValue = random.NextDouble() * (options.MaxIncrement - options.MinIncrement) + options.MinIncrement;
        return new(metric.Value + randomValue);
    }
}