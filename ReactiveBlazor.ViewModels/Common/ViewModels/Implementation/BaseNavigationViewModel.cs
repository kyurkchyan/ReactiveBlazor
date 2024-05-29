using ReactiveBlazor.ViewModels.Common.Extensions;

namespace ReactiveBlazor.ViewModels.Common.ViewModels.Implementation;

public abstract class BaseNavigationViewModel : BaseDisposableViewModel, INavigationViewModel
{
    public Interaction<DialogViewModel, bool> ShowDialog { get; } = new();
    public Interaction<AsyncConfirmDialogViewModel, bool> ShowAsyncConfirmDialog { get; } = new();
    public Interaction<Snackbar, Unit> ShowSnackbar { get; } = new();

    protected IDisposable ShowFailureDialogOnError(ILogger logger, params IHandleObservableErrors[] items)
        => ShowFailureDialogOnError(logger, "Something went wrong. Please try again.", items);

    protected IDisposable ShowFailureDialogOnError(ILogger logger, string message, params IHandleObservableErrors[] items)
        => ShowFailureDialogOnError(logger, new DialogViewModel("Error", message, "Ok"), items);

    protected IDisposable ShowFailureDialogOnError(ILogger logger, DialogViewModel dialog, params IHandleObservableErrors[] items)
    {
        return new CompositeDisposable(items.Select(item => item.ThrownExceptions
                                                                .Select(ex => ShowDialog.Handle(dialog))
                                                                .Switch()
                                                                .SubscribeSafe(logger)));
    }
}
