using System.Reactive.Linq;
using ReactiveUI;

namespace ReactiveBlazor.Client.ReactiveUI;

public class ActivationAfterRenderForViewFetcher : IActivationForViewFetcher
{
    public int GetAffinityForView(Type view)
        => typeof(ICanRender).IsAssignableFrom(view) ? 100 : 0;

    public IObservable<bool> GetActivationForView(IActivatableView view)
    {
        var activation =
            view is ICanRender canRenderView
                ? GetActivationFor(canRenderView)
                : Observable.Never<bool>();
        return activation.DistinctUntilChanged();
    }

    private static IObservable<bool> GetActivationFor(ICanRender view)
    {
        var rendered = view.Rendered.Select(_ => true);
        var disposed = view.Disposed.Select(_ => false);

        var activation = rendered.Merge(disposed);

        if (view is ICanActivate canActivate)
        {
            activation = activation.Merge(canActivate.Deactivated.Select(_ => false));
        }

        return activation;
    }
}
