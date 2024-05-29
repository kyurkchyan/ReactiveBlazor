namespace ReactiveBlazor.ViewModels.Common.ViewModels;

public interface INavigationViewModel : IDisposableViewModel
{
    public Interaction<DialogViewModel, bool> ShowDialog { get; }
    public Interaction<AsyncConfirmDialogViewModel, bool> ShowAsyncConfirmDialog { get; }
    public Interaction<Snackbar, Unit> ShowSnackbar { get; }
}
