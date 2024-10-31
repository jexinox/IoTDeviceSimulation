using System;
using System.Text.Json;
using System.Threading.Tasks;
using MQTTnet;

namespace IoTDeviceSimulation.Metrics.Publishing;

public class MqttMetricPublisher(MqttMetricPublisherOptions options) : IDisposable
{
    private readonly Lazy<Task<IMqttClient>> mqttClient = new(() => CreateMqttClient(options));

    public async Task PublishMetric(Metric metric)
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(options.Topic)
            .WithPayload(JsonSerializer.Serialize(metric))
            .Build();

        var client = await mqttClient.Value;
        
        await client.PublishAsync(message);
    }
    
    private static async Task<IMqttClient> CreateMqttClient(MqttMetricPublisherOptions options)
    {
        var factory = new MqttClientFactory();
        
        var clientOptions = factory
            .CreateClientOptionsBuilder()
            .WithClientId(options.ClientId)
            .WithTcpServer(options.Host, options.Port)
            .Build();

        var mqttClient = factory.CreateMqttClient();
        
        await mqttClient.ConnectAsync(clientOptions);
        
        return mqttClient;
    }

    public void Dispose() => mqttClient.Value.Dispose();
}