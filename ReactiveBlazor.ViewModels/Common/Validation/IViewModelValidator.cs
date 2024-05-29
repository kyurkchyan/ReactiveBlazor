using System.Collections;

namespace ReactiveBlazor.ViewModels.Common.Validation;

public interface IViewModelValidator : IDisposable
{
    IObservable<bool> Valid { get; }
    IEnumerable GetErrors(string? propertyName);
    public string[] GetAllErrors();
    public string[] GetPropertyErrors(string propertyName);
}
