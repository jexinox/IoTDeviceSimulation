using System;
using System.Threading.Tasks;
using MQTTnet;

namespace IoTDeviceSimulation.Mqtt;

public class MqttSingleton
{
    public static readonly Lazy<Task<IMqttClient>> Instance = new(CreateMqttClient);

    private static async Task<IMqttClient> CreateMqttClient()
    {
        var factory = new MqttClientFactory();
        
        var clientOptions = factory
            .CreateClientOptionsBuilder()
            .WithClientId("mqtt-jexinox-qo2thh")
            .WithTcpServer("dev.rightech.io", 1883)
            .Build();

        var mqttClient = factory.CreateMqttClient();
        
        await mqttClient.ConnectAsync(clientOptions);
        
        return mqttClient;
    }
}