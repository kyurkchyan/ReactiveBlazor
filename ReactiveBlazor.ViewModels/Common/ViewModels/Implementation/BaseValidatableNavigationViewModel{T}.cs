namespace ReactiveBlazor.ViewModels.Common.ViewModels.Implementation;

public abstract class BaseValidatableNavigationViewModel<TInterface> : BaseValidatableNavigationViewModel
{
    public override Type Interface => typeof(TInterface);
}
