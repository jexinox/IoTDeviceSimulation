using System;
using System.Reactive.Linq;

namespace IoTDeviceSimulation.Metrics.Publishing;

public class MetricPublishingOperator(MqttMetricPublisher publisher)
{
    public IAsyncObservable<Metric> Apply(IAsyncObservable<Metric> observable)
    {
        return observable.Do(async metric => await publisher.PublishMetric(metric));
    }
}