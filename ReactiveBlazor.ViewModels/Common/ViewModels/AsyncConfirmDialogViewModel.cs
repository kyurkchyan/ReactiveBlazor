namespace ReactiveBlazor.ViewModels.Common.ViewModels;

/// <summary>
///     A view model for showing a confirmation dialog with async confirm action.
/// </summary>
public class AsyncConfirmDialogViewModel(string title,
                                         string description,
                                         string confirmButtonText,
                                         string cancelButtonText,
                                         DialogConfirmActionType confirmActionType,
                                         ReactiveCommand<Unit, bool> confirmCommand)
    : DialogViewModel(title,
                      description,
                      confirmButtonText,
                      cancelButtonText)
{
    public DialogConfirmActionType ConfirmActionType { get; } = confirmActionType;
    public ReactiveCommand<Unit, bool> ConfirmCommand { get; } = confirmCommand;
}
