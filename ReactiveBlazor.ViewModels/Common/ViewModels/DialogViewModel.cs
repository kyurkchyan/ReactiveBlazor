namespace ReactiveBlazor.ViewModels.Common.ViewModels;

/// <summary>
///     A view model for showing a dialog.
///     Dialogs can have a title, description, and a cancel and confirm button.
/// </summary>
public class DialogViewModel : ReactiveObject
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DialogViewModel" /> class.
    /// </summary>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="description">A more detailed description about the dialog.</param>
    /// <param name="cancelButtonText">The text of the cancel button.</param>
    /// <param name="confirmButtonText">The text of the confirm button.</param>
    public DialogViewModel(string title,
                           string description,
                           string confirmButtonText,
                           string? cancelButtonText = null)
    {
        Title = title;
        Description = description;
        CancelButtonText = cancelButtonText;
        ConfirmButtonText = confirmButtonText;
    }

    public static DialogViewModel DefaultTryAgainDialog => new("Error", "Something went wrong. Please try again.", "OK");

    /// <summary>
    ///     Gets the title of the dialog.
    /// </summary>
    public string Title { get; }

    /// <summary>
    ///     Gets a detailed description of the dialog.
    /// </summary>
    public string Description { get; }

    /// <summary>
    ///     Gets the text for the cancellation button.
    /// </summary>
    public string? CancelButtonText { get; }

    /// <summary>
    ///     Gets the text for the confirmation button.
    /// </summary>
    public string ConfirmButtonText { get; }
}
