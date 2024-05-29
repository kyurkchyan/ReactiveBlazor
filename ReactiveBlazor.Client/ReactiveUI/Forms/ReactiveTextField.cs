using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ReactiveBlazor.Client.ReactiveUI.Forms;

public class ReactiveTextField<T> : MudTextField<T>
{
    [Parameter] public string? ValidationError { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        Error = !string.IsNullOrWhiteSpace(ValidationError);
        ErrorText = ValidationError;
    }
}