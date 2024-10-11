using System;
using System.Reactive.Linq;
using System.Threading;
using IoTDeviceSimulation.Metrics.Generation;
using IoTDeviceSimulation.Metrics.Update.Options;

namespace IoTDeviceSimulation.Metrics.Update;

public class MetricUpdater(
    IMetricGeneratorProvider metricGenerator,
    IMetricUpdateOptionsProvider optionsProvider,
    CancellationTokenSource cancellationTokenSource) : IObservable<Metric>
{
    private readonly Lazy<IObservable<Metric>> _internalObservable = 
        new(() => CreateInternalObservable(new(Metric.Default, optionsProvider, metricGenerator, cancellationTokenSource.Token)));

    public IDisposable Subscribe(IObserver<Metric> observer) => _internalObservable.Value.Subscribe(observer);

    private static IObservable<Metric> CreateInternalObservable(MetricUpdateState initialState) =>
        Observable.Generate(
            initialState,
            state => !state.CancellationToken.IsCancellationRequested,
            state => state with { Metric = state.GeneratorProvider.Get().Generate(state.Metric) },
            state => state.Metric,
            state => state.OptionsProvider.Get().IntervalBetweenUpdates);

    private record MetricUpdateState(
        Metric Metric,
        IMetricUpdateOptionsProvider OptionsProvider,
        IMetricGeneratorProvider GeneratorProvider,
        CancellationToken CancellationToken);
}