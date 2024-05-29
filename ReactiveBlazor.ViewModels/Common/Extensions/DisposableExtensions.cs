using System.Diagnostics;

namespace ReactiveBlazor.ViewModels.Common.Extensions;

/// <summary>
///     The DisposableExtensions class contains extension methods that help
///     in logging the disposal of objects, particularly within the context of a Model-View-ViewModel (MVVM) pattern.
///     This class is particularly useful in a debugging scenario to ensure that
///     view models and associated disposables are being properly disposed of,
///     which is critical for preventing memory leaks in applications.
/// </summary>
public static class DisposableExtensions
{
    public static void LogDisposal(this CompositeDisposable disposables, string message)
    {
#if DEBUG
        Disposable.Create(() => Debug.WriteLine(message))
                  .DisposeWith(disposables);
#endif
    }
}
