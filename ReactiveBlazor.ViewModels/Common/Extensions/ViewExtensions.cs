using Splat;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace ReactiveBlazor.ViewModels.Common.Extensions;

public static class ViewExtensions
{
    public static IDisposable BindDisposable<T>(this IViewFor<T> @this, Func<T, IDisposable> disposableFactory) where T : class, IReactiveObject =>
        @this.WhenAnyValue(x => x.ViewModel)
             .WhereNotNull()
             .Select(viewModel => Observable.Create<IDisposable>(observer =>
             {
                 var registration = disposableFactory(viewModel);
                 observer.OnNext(registration);
                 return registration;
             }))
             .Switch()
             .Subscribe();

    public static void InvokeCommand<TViewModel, TResult>(this IViewFor<TViewModel> @this,
                                                          Func<TViewModel, ReactiveCommand<Unit, TResult>> commandFactory,
                                                          ILogger logger,
                                                          CompositeDisposable disposables) where TViewModel : class =>
        @this.WhenAnyValue(p => p.ViewModel)
             .WhereNotNull()
             .Select(viewModel => Observable.Return(Unit.Default)
                                            .InvokeCommand(commandFactory(viewModel))
                                            .DisposeWith(disposables))
             .SubscribeSafe(logger)
             .DisposeWith(disposables);

    public static IObservable<bool> GetIsActivated(this IActivatableView @this) =>
        Locator.Current.GetService<IActivationForViewFetcher>()!
               .GetActivationForView(@this)
               .Replay(1)
               .RefCount();
}
