namespace ReactiveBlazor.Client.ReactiveUI.Forms;

public static class Extensions
{
    public static void UpdateValidation(this IReactiveField field)
    {
        var errors = field.ValidationError?
                          .Split(SeparatorJoinedValidationTextFormatter.DefaultSeparator)
                          .Where(error => !string.IsNullOrWhiteSpace(error))
                          .ToList();
        var error = errors?.FirstOrDefault();
        field.ValidationErrors = errors ?? [];
        field.ValidationError = error;
        field.Error = !string.IsNullOrWhiteSpace(error);
        field.ErrorText = field.Error ? error : null;
        field.ErrorId = field.Error ? Guid.NewGuid().ToString() : null;
    }
}
