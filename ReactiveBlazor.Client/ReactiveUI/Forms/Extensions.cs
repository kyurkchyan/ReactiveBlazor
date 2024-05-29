namespace ReactiveBlazor.Client.ReactiveUI.Forms;

public static class Extensions
{
    public static void UpdateValidation(this IReactiveField field)
    {
        var errors = field.ValidationError?.Split(SeparatorJoinedValidationTextFormatter.DefaultSeparator).ToList();
        var error = errors?.FirstOrDefault();
        field.ValidationErrors = errors ?? [];
        field.ValidationError = error;
        field.Error = !string.IsNullOrWhiteSpace(error);
        field.ErrorText = error;
    }
}
