using System;
using IoTDeviceSimulation.Metrics.Update.Generation.Options;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public class ActuatorMetricGeneratorFactoryDecorator(
    IMetricGeneratorFactory metricGeneratorFactory,
    IActuatorOptionsProvider actuatorOptionsProvider) : IMetricGeneratorFactory
{
    public IMetricGenerator Create(MetricGeneratorOptions options)
    {
        var generator = metricGeneratorFactory.Create(options);
        var actuatorOptions = actuatorOptionsProvider.Get();
        return DecorateGeneratorWithActuator(generator, actuatorOptions);
    }

    private IMetricGenerator DecorateGeneratorWithActuator(IMetricGenerator generator, IActuatorOptions actuatorOptions)
    {
        return actuatorOptions switch
        {
            AutoActuatorOptions autoActuatorOptions => 
                new AutoActuatorMetricGeneratorDecorator(autoActuatorOptions, generator),
            ManualActuatorOptions => generator,
            _ => throw new ArgumentOutOfRangeException(nameof(actuatorOptions), actuatorOptions, null),
        };
    }

    private class AutoActuatorMetricGeneratorDecorator(AutoActuatorOptions options, IMetricGenerator generator) : IMetricGenerator
    {
        public MetricGeneratorType Type { get; } = generator.Type;
        
        public Metric Generate(Metric metric)
        {
            var generatedMetric = generator.Generate(metric);
            return generatedMetric.Value >= options.MetricValueLimit 
                ? new(generatedMetric.Value - options.MetricChange) 
                : generatedMetric;
        }
    }
}