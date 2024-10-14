using System;
using System.Reactive.Linq;
using IoTDeviceSimulation.Metrics.Update.Generation.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class MetricGeneratorOperator
{
    public IObservable<IMetricGenerator> Apply(IObservable<MetricGeneratorOptions> optionsStream)
    {
        return optionsStream
            .Select(options => options.Type)
            .Select(CreateNewGeneratorByType);
    }

    private static IMetricGenerator CreateNewGeneratorByType(MetricGeneratorType type) =>
        type switch
        {
            MetricGeneratorType.Linear => new LinearMetricGenerator(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
}