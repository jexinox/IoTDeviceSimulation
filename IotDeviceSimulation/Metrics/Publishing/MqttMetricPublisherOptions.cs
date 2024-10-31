namespace IoTDeviceSimulation.Metrics.Publishing;

public record MqttMetricPublisherOptions(
    string ClientId = "IoTDeviceSimulation.MqttMetricPublisher",
    string Topic = "a.karpov/metrics",
    string Host = "test.mosquitto.org",
    int Port = 1883);