using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace IoTDeviceSimulation.Metrics.Publishing;

public class FirestoreMetricPublisher(FirestoreDb db)
{
    private readonly CollectionReference collection = db.Collection("metrics");

    public async Task Publish(Metric metric)
    {
        await collection
            .Document(DateTime.UtcNow.ToString("O"))
            .SetAsync(ConvertToFirestoreMetric(metric));
    }

    private object ConvertToFirestoreMetric(Metric metric)
    {
        return new Dictionary<string, object>
        {
            ["value"] = metric.Value,
        };
    }
}