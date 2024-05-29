using ReactiveUI.Validation.Collections;
using ReactiveUI.Validation.Formatters.Abstractions;

namespace ReactiveBlazor.Client.ReactiveUI;

public class FirstErrorValidationTextFormatter(string? separator = null) : IValidationTextFormatter<string>
{
    public static FirstErrorValidationTextFormatter Default { get; } = new();

    public string Format(IValidationText validationText) => validationText.FirstOrDefault() ?? "";
}
