namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public record AutoActuatorOptions(double MetricValueLimit, double MetricChange) : IActuatorOptions;