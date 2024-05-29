using System.Runtime.CompilerServices;

namespace ReactiveBlazor.ViewModels.Common.Extensions;

public static class ReactiveExtensions
{
    public static IObservable<bool> Negate(this IObservable<bool> @this) =>
        @this.Select(x => !x);

    public static IObservable<bool> AllTrue(this IObservable<IList<bool>> @this) =>
        @this.Select(x => x.All(item => item));

    public static IObservable<Unit> ToUnit<T>(this IObservable<T> @this) =>
        @this.Select(_ => Unit.Default);

    public static IDisposable SubscribeSafe<T>(this IObservable<T> @this,
                                               ILogger logger,
                                               string callerMemberName = "",
                                               [CallerFilePath] string callerFilePath = "",
                                               [CallerLineNumber] int callerLineNumber = 0) =>
        @this.Subscribe(_ => { },
                        ex =>
                        {
                            logger.LogError(ex,
                                            "Unhandled exception in {CallerMemberName} ({CallerFilePath}:{CallerLineNumber})",
                                            callerMemberName,
                                            callerFilePath,
                                            callerLineNumber);
                            RxApp.DefaultExceptionHandler.OnNext(ex);
                        });

    public static IDisposable SubscribeSafe<T>(this IObservable<T> @this,
                                               Action<T> onNext,
                                               ILogger logger,
                                               string callerMemberName = "",
                                               [CallerFilePath] string callerFilePath = "",
                                               [CallerLineNumber] int callerLineNumber = 0) =>
        @this.Subscribe(onNext,
                        ex =>
                        {
                            logger.LogError(ex,
                                            "Unhandled exception in {CallerMemberName} ({CallerFilePath}:{CallerLineNumber})",
                                            callerMemberName,
                                            callerFilePath,
                                            callerLineNumber);
                            RxApp.DefaultExceptionHandler.OnNext(ex);
                        });
}
