using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ReactiveBlazor.Client.ReactiveUI.Forms;

public class ReactiveDatePicker : MudDatePicker, IReactiveField
{
    [Parameter] public string? ValidationError { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        this.UpdateValidation();
        FieldChanged(Date);
    }
}