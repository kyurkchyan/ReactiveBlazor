using FluentValidation.Results;
using ReactiveUI.Validation.Collections;
using ReactiveUI.Validation.States;

namespace ReactiveBlazor.ViewModels.Common.Validation;

internal class FluentValidationState : IValidationState
{
    public FluentValidationState(ValidationResult result)
    {
        Text = ValidationText.Create(result.Errors.Select(error => error.ErrorMessage));
        IsValid = result.IsValid;
    }

    public IValidationText Text { get; }
    public bool IsValid { get; }
}
