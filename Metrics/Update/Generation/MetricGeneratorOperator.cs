using System;
using System.Reactive.Linq;
using IoTDeviceSimulation.Metrics.Update.Generation.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class MetricGeneratorOperator(
    IObservable<IMetricGeneratorOptions> optionsStream, IMetricGeneratorFactory metricGeneratorFactory)
{
    public IObservable<IMetricGenerator> Apply()
    {
        return optionsStream
            .Select(CreateNewGeneratorByType);
    }

    private IMetricGenerator CreateNewGeneratorByType(IMetricGeneratorOptions options) =>
        options.Accept(metricGeneratorFactory);
}