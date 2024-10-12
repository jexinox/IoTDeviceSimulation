using System;
using System.Collections.Generic;
using System.Linq;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class MetricGeneratorFactory(
    IEnumerable<IMetricGenerator> generators) : IMetricGeneratorFactory
{
    private readonly Lazy<Dictionary<MetricGeneratorType, IMetricGenerator>> _generators = 
        new(() => GetGeneratorsLookup(generators));

    public IMetricGenerator Create(MetricGeneratorOptions options)
    {
        return _generators.Value[options.Type];
    }

    private static Dictionary<MetricGeneratorType, IMetricGenerator> GetGeneratorsLookup(
        IEnumerable<IMetricGenerator> generators)
    {
        return generators
            .ToLookup(generator => generator.Type, generator => generator)
            .ToDictionary(grouping => grouping.Key, IMetricGenerator (grouping) => new CumulativeMetricGenerator(grouping.Key, grouping));
    }

    private class CumulativeMetricGenerator(
        MetricGeneratorType type, IEnumerable<IMetricGenerator> generators) : IMetricGenerator
    {
        public MetricGeneratorType Type { get; } = type;

        public Metric Generate(Metric metric)
        {
            return generators.Aggregate(metric, (current, generator) => generator.Generate(current));
        }
    }
}