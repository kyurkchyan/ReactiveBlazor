using Microsoft.AspNetCore.Components;
using MudBlazor;
using ReactiveBlazor.ViewModels.Common.Validation;

namespace ReactiveBlazor.Client.ReactiveUI;

public class ReactiveForm<T> : MudForm
    where T : class, IValidatableViewModel
{
    private T? _viewModel;

    public ReactiveForm()
    {
        ValidationDelay = 0;
    }

    [Parameter] public T? ViewModel { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (_viewModel == ViewModel) return;
        Model = _viewModel = ViewModel;
        if (_viewModel != null)
        {
            Validation = (object _, string property) =>
            {
                var errors = _viewModel.GetPropertyErrors(property);
                return errors;
            };
        }
    }
}