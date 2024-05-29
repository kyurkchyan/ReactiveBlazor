namespace ReactiveBlazor.Client.ReactiveUI.Forms;

public static class Extensions
{
    public static void UpdateValidation(this IReactiveField field)
    {
        var error = field.ValidationError;
        field.ValidationError = error;
        field.Error = !string.IsNullOrWhiteSpace(error);
        field.ErrorText = error;
    }
}