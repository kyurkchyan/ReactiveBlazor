using System.Reactive;

namespace ReactiveBlazor.Client.ReactiveUI;

public interface ICanRender
{
    /// <summary>
    ///     Gets a observable which is triggered when the View is rendered.
    /// </summary>
    IObservable<Unit> Rendered { get; }

    /// <summary>
    ///     Gets a observable which is triggered when the View is disposed.
    /// </summary>
    IObservable<Unit> Disposed { get; }
}
