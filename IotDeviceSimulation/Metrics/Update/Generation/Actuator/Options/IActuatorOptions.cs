using System.Threading.Tasks;

namespace IoTDeviceSimulation.Metrics.Update.Generation.Actuator.Options;

public interface IActuatorOptions
{ 
    Task<IActuator> Get(IActuatorFactory factory);
}