using System;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public class ActuatorFactory : IActuatorFactory
{
    public IActuator GetManualActuator(ManualActuatorOptions actuatorOptions)
    {
        return new StubActuator();
    }

    public IActuator GetAutoActuator(AutoActuatorOptions actuatorOptions)
    {
        return new StubActuator();
    }
    
    private class StubActuator : IActuator
    {
        public Metric Actuate(Metric metric)
        {
            Console.WriteLine("Actuator called");
            return metric;
        }
    }
}