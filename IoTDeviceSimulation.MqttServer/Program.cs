using System.Text.Json;
using IoTDeviceSimulation.MqttServer;
using MQTTnet;
using MQTTnet.Server;

var mqttServerFactory = new MqttServerFactory();
var serverOptions = new MqttServerOptionsBuilder().WithDefaultEndpoint().Build();

var server = mqttServerFactory.CreateMqttServer(serverOptions);

server.ClientSubscribedTopicAsync += CreateHandler;

await server.StartAsync();

async Task CreateHandler(ClientSubscribedTopicEventArgs eventArgs)
{
    var clientId = eventArgs.ClientId;
    Console.WriteLine($"ClientId: {clientId}");
    if (clientId is null || clientId.StartsWith("MqttServerHandler")) return;

    var factory = new MqttClientFactory();
    var clientOptions = factory
        .CreateClientOptionsBuilder()
        .WithClientId($"MqttServerHandler-{Guid.NewGuid()}")
        .Build();
    var client = factory.CreateMqttClient();
    var topic = eventArgs.TopicFilter.Topic;
    client.ApplicationMessageReceivedAsync += async messageRecievedEventArgs =>
    {
        Console.WriteLine("Connected");
        var metric = JsonSerializer.Deserialize<MetricMessage>(messageRecievedEventArgs.ApplicationMessage.Payload.ToString());
        if (metric?.Value > 0.7)
        {
            var changeMetricMessage = new ChangeMetricMessage(0.2);
            var json = JsonSerializer.Serialize(changeMetricMessage);
            var publishMessage = factory.CreateApplicationMessageBuilder().WithTopic(topic).WithPayload(json).Build();
            await client.PublishAsync(publishMessage);
            Console.WriteLine("Publish Success");
        }
    };
    await client.SubscribeAsync(eventArgs.TopicFilter.Topic);
}

namespace IoTDeviceSimulation.MqttServer
{
    public record MetricMessage(double Value);

    public record ChangeMetricMessage(double Change);
}