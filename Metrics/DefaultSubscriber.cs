using System;
using System.Collections.Generic;

namespace IoTDeviceSimulation.Metrics;

public class DefaultSubscriber<T>(IEnumerable<IObserver<T>> observers, IEnumerable<IObservable<T>> observables) : ISubscriber
{
    public void Subscribe()
    {
        foreach (var observer in observers)
        {
            foreach (var observable in observables)
            {
                observable.Subscribe(observer);
            }
        }
    }
}