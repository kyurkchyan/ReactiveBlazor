namespace ReactiveBlazor.ViewModels.Common.ViewModels.Implementation;

public abstract class BaseValidatableViewModel<TInterface> : BaseValidatableViewModel
{
    public override Type Interface => typeof(TInterface);
}
