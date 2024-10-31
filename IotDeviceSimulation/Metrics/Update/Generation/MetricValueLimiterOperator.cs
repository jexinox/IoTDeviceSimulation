using System;
using System.Reactive.Linq;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class MetricValueLimiterOperator
{
    public IAsyncObservable<IMetricGenerator> Apply(IAsyncObservable<IMetricGenerator> source)
    {
        return source.Select(metricGenerator => new MetricValueLimiter(metricGenerator));
    }
    
    private class MetricValueLimiter(IMetricGenerator generator) : IMetricGenerator
    {
        public Metric Generate(Metric metric)
        {
            var generated = generator.Generate(metric);
            return generated.Value < 0 
                ? new() 
                : generated;
        }
    }
}