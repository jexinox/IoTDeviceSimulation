using System.Text;
using System.Text.Json;
using MQTTnet;

var metricsTopic = "a.karpov/metrics";
var changesTopic = "a.karpov/changes";

var clientFactory = new MqttClientFactory();
var clientOptions = clientFactory
    .CreateClientOptionsBuilder()
    .WithClientId("IoTDeviceSimulation.MqttAutoClient")
    .WithTcpServer("test.mosquitto.org", 1883)
    .Build();

var mqttClient = clientFactory.CreateMqttClient();

mqttClient.ApplicationMessageReceivedAsync += async args =>
{
    var payload = Encoding.UTF8.GetString(args.ApplicationMessage.Payload);
    var metric = JsonSerializer.Deserialize<MetricMessage>(payload);
    Console.WriteLine(metric);
    if (metric is null)
    {
        Console.WriteLine($"Unknown metric {payload}");
        return;
    }

    if (metric.Value > 0.7)
    {
        var changeMessage = clientFactory
            .CreateApplicationMessageBuilder()
            .WithTopic(changesTopic)
            .WithPayload(JsonSerializer.Serialize(new ChangeMetricMessage(0.2)))
            .Build();
        await mqttClient.PublishAsync(changeMessage);
    }
};

await mqttClient.ConnectAsync(clientOptions);

var subscribeOptions = clientFactory
    .CreateSubscribeOptionsBuilder()
    .WithTopicFilter(metricsTopic)
    .Build();

await mqttClient.SubscribeAsync(subscribeOptions);

public record MetricMessage(double Value);

public record ChangeMetricMessage(double Change);