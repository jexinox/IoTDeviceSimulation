using System.Text;
using System.Text.Json;
using MQTTnet;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using File = System.IO.File;

const string changesTopic = "a.karpov/changes";
const string metricsTopic = "a.karpov/metrics";

var clientFactory = new MqttClientFactory();
var clientOptions = clientFactory
    .CreateClientOptionsBuilder()
    .WithClientId("IoTDeviceSimulation.TgBot")
    .WithTcpServer("test.mosquitto.org", 1883)
    .Build();

var mqttClient = clientFactory.CreateMqttClient();
await mqttClient.ConnectAsync(clientOptions);

var token = File.ReadAllText("token");
var bot = new TelegramBotClient(token);
var chats = new List<long>();

bot.OnMessage += async (message, type) =>
{
    if (type != UpdateType.Message)
    {
        return;
    }
    
    var text = message.Text;
    if (text is null)
    {
        return;
    }

    if (text.StartsWith("/start"))
    {
        chats.Add(message.Chat.Id);
    }
    
    if (text.StartsWith("/mqttsend"))
    {
        var messageParts = text.Split(" ");
        if (messageParts.Length > 2)
        {
            return;
        }

        if (!double.TryParse(messageParts[1], out var metricChange))
        {
            await bot.SendMessage(message.Chat.Id, "Please enter a valid metric change.");
            return;
        }
        
        var mqttMessage = clientFactory
            .CreateApplicationMessageBuilder()
            .WithTopic(changesTopic)
            .WithPayload(JsonSerializer.Serialize(new ChangeMetricMessage(metricChange)))
            .Build();
        
        await mqttClient.PublishAsync(mqttMessage);
    }
};

mqttClient.ApplicationMessageReceivedAsync += async (args) =>
{
    var payload = Encoding.UTF8.GetString(args.ApplicationMessage.Payload);
    var metric = JsonSerializer.Deserialize<MetricMessage>(payload);

    if (metric is null)
    {
        await SendMessageToAllChats($"Unknown metric received {payload}");
    }
    else
    {
        await SendMessageToAllChats($"Received metric: {metric}");
    }
};

var subscribeOptions = clientFactory
    .CreateSubscribeOptionsBuilder()
    .WithTopicFilter(metricsTopic)
    .Build();

await mqttClient.SubscribeAsync(subscribeOptions);

await bot.SetMyCommands([new() { Command = "/mqttsend", Description = "Sends mqtt message to IoTDeviceSimulator" }]);

Console.ReadLine();

await mqttClient.DisconnectAsync();

async Task SendMessageToAllChats(string message)
{
    foreach (var chat in chats)
    {
        await bot.SendMessage(new(chat), message);
    }
}

public record MetricMessage(double Value);

public record ChangeMetricMessage(double Change);