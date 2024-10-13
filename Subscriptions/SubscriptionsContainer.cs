using System;
using System.Collections.Generic;
using System.Reactive.Disposables;

namespace IoTDeviceSimulation.Subscriptions;

public class SubscriptionsContainer<T>
{
    private readonly HashSet<IObserver<T>> _observers = new();
    private readonly CompositeDisposable compositeSubscription = new();
    
    public IReadOnlySet<IObserver<T>> Observers => _observers;
    
    public IDisposable Subscribe(IObserver<T> observer, params Action[] callbacks)
    {
        _observers.Add(observer);
        var subscription = Disposable.Create(() =>
        {
            _observers.Remove(observer);
            foreach (var callback in callbacks)
            {
                callback();
            }
        });
        compositeSubscription.Add(subscription);
        return subscription;
    }

    public void UnsubscribeAll()
    {
        compositeSubscription.Dispose();
    }
}