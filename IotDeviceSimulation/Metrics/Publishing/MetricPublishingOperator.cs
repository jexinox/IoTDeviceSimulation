using System;
using System.Reactive.Linq;

namespace IoTDeviceSimulation.Metrics.Publishing;

public class MetricPublishingOperator(MqttMetricPublisher publisher, FirestoreMetricPublisher firestorePublisher)
{
    public IAsyncObservable<Metric> Apply(IAsyncObservable<Metric> observable)
    {
        return observable
            .Do(async metric => await publisher.PublishMetric(metric))
            .Do(async metric => await firestorePublisher.Publish(metric));
    }
}