using ReactiveUI.Validation.Collections;
using ReactiveUI.Validation.Formatters.Abstractions;

namespace ReactiveBlazor.Client.ReactiveUI;

public class SeparatorJoinedValidationTextFormatter(
    string separator = SeparatorJoinedValidationTextFormatter.DefaultSeparator) : IValidationTextFormatter<string>
{
    public const string DefaultSeparator = "|";
    public static SeparatorJoinedValidationTextFormatter Default { get; } = new();

    public string Format(IValidationText validationText) => string.Join(separator, validationText);
}