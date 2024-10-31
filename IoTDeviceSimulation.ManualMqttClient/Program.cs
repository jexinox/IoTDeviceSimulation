using System.Text.Json;
using MQTTnet;

var topic = "a.karpov/changes";

var clientFactory = new MqttClientFactory();
var clientOptions = clientFactory
    .CreateClientOptionsBuilder()
    .WithClientId("IoTDeviceSimulation.MqttManualClient")
    .WithTcpServer("test.mosquitto.org", 1883)
    .Build();

var mqttClient = clientFactory.CreateMqttClient();

await mqttClient.ConnectAsync(clientOptions);
while (true) 
{
    Console.WriteLine("Press enter to send a message to MQTT broker. Metric change = 0.2");
    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
    var message = clientFactory
        .CreateApplicationMessageBuilder()
        .WithTopic(topic)
        .WithPayload(JsonSerializer.Serialize(new ChangeMetricMessage(0.2)))
        .Build();
    await mqttClient.PublishAsync(message);
}

public record ChangeMetricMessage(double Change);