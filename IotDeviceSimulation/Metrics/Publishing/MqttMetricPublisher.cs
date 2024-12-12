using System;
using System.Text.Json;
using System.Threading.Tasks;
using IoTDeviceSimulation.Mqtt;
using MQTTnet;

namespace IoTDeviceSimulation.Metrics.Publishing;

public class MqttMetricPublisher(MqttMetricPublisherOptions options)
{
    public async Task PublishMetric(Metric metric)
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(options.Topic)
            .WithPayload(JsonSerializer.Serialize(metric))
            .Build();

        var client = await MqttSingleton.Instance.Value;
        
        await client.PublishAsync(message);
    }
}