using System.ComponentModel;
using ReactiveUI.Validation.Contexts;

namespace ReactiveBlazor.ViewModels.Common.Validation;

public interface IValidatableViewModel : ReactiveUI.Validation.Abstractions.IValidatableViewModel,
                                         INotifyDataErrorInfo,
                                         IReactiveObject,
                                         IDisposable
{
    void RaiseErrorsChanged(object? sender, DataErrorsChangedEventArgs args);
    void SetHasErrors(bool value);
    void SetValidationContext(ValidationContext context);
    public string[] GetAllErrors();
    public string[] GetPropertyErrors(string propertyName);
}
