using System.Collections;
using System.ComponentModel;
using ReactiveBlazor.ViewModels.Common.Validation;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Contexts;

namespace ReactiveBlazor.ViewModels.Common.ViewModels.Implementation;

public abstract class BaseValidatableViewModel : BaseDisposableViewModel, IValidatableViewModel
{
    protected IViewModelValidator Validator { get; init; } = null!;

    public IValidationContext ValidationContext { get; private set; } = null!;

    public void RaiseErrorsChanged(object? sender, DataErrorsChangedEventArgs args)
    {
        ErrorsChanged?.Invoke(sender, args);
    }

    public void SetHasErrors(bool value)
    {
        HasErrors = value;
    }

    public void SetValidationContext(ValidationContext context)
    {
        ValidationContext = context;
    }

    public string[] GetAllErrors() => Validator.GetAllErrors();

    public string[] GetPropertyErrors(string propertyName) => Validator.GetPropertyErrors(propertyName);
    public Func<object, string, Task<IEnumerable<string>>> ValidateProperty => Validator.ValidateProperty;

    public IEnumerable GetErrors(string? propertyName) => Validator.GetErrors(propertyName);

    [Reactive]
    public bool HasErrors { get; private set; }

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Validator.Dispose();
            ValidationContext.Dispose();
        }

        base.Dispose(disposing);
    }
}
