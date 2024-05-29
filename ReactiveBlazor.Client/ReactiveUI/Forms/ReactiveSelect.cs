using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ReactiveBlazor.Client.ReactiveUI.Forms;

public class ReactiveSelect<T> : MudSelect<T>, IReactiveField
{
    [Parameter]
    public string? ValidationError { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        this.UpdateValidation();
    }
}
