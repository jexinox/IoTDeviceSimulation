using System;
using System.Reactive.Linq;
using IoTDeviceSimulation.Metrics.Update.Generation.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class MetricGeneratorsProvider(
    IObservable<IMetricGeneratorOptions> optionsStream, IMetricGeneratorFactory metricGeneratorFactory)
{
    public IObservable<IMetricGenerator> Get()
    {
        return optionsStream
            .Select(CreateNewGeneratorByType);
    }

    private IMetricGenerator CreateNewGeneratorByType(IMetricGeneratorOptions options) =>
        options.Accept(metricGeneratorFactory);
}