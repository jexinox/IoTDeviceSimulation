using System;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator;

public class ActuatorFactory : IActuatorFactory
{
    public IActuator GetManualActuator(ManualActuatorOptions actuatorOptions)
    {
        return new StubActuator("manual");
    }

    public IActuator GetAutoActuator(AutoActuatorOptions actuatorOptions)
    {
        return new AutoActuator(actuatorOptions);
    }
    
    private class StubActuator(string name) : IActuator
    {
        public Metric Actuate(Metric metric)
        {
            Console.WriteLine($"Actuator called {name}");
            return metric;
        }
    }
}