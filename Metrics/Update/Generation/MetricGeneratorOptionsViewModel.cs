using System;
using ReactiveUI;

namespace IoTDeviceSimulation.Metrics.Update.Generation;

public class MetricGeneratorOptionsViewModel : ReactiveObject, IObservable<MetricGeneratorOptions>
{
    public IDisposable Subscribe(IObserver<MetricGeneratorOptions> observer)
    {
        throw new NotImplementedException();
    }
}