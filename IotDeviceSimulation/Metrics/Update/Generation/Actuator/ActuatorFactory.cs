using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Auto;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Manual;
using IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Mqtt;
using MQTTnet;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public class ActuatorFactory : IActuatorFactory
{
    public IActuator GetManualActuator(ManualActuatorOptions actuatorOptions)
    {
        return new ManualActuator(actuatorOptions);
    }

    public IActuator GetAutoActuator(AutoActuatorOptions actuatorOptions)
    {
        return new AutoActuator(actuatorOptions);
    }

    public async Task<IActuator> GetMqttActuator(MqttActuatorOptions mqttActuatorOptions)
    {
        Console.WriteLine(mqttActuatorOptions);
        var clientFactory = new MqttClientFactory();
        var clientOptions = clientFactory
            .CreateClientOptionsBuilder()
            .WithClientId(mqttActuatorOptions.ClientId)
            .WithTcpServer(mqttActuatorOptions.Host, mqttActuatorOptions.Port)
            .Build();
        var client = clientFactory.CreateMqttClient();
        var actuator = new MqttActuator();
        client.ApplicationMessageReceivedAsync += args =>
        {
            var serializedMessage = Encoding.UTF8.GetString(args.ApplicationMessage.Payload);
            Console.WriteLine(serializedMessage);
            var message = JsonSerializer.Deserialize<MqttMetricChange>(serializedMessage);
            if (message is null)
            {
                Console.WriteLine($"couldn't deserialize message {serializedMessage}");
                return Task.CompletedTask;
            }

            actuator.OnMetricChangesRecieved(message);
            return Task.CompletedTask;
        };
        await client.ConnectAsync(clientOptions);
        await client.SubscribeAsync(mqttActuatorOptions.Topic);
        return actuator;
    }
}