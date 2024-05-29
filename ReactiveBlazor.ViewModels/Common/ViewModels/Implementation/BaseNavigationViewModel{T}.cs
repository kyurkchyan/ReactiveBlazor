namespace ReactiveBlazor.ViewModels.Common.ViewModels.Implementation;

public abstract class BaseNavigationViewModel<TInterface> : BaseNavigationViewModel
{
    public override Type Interface => typeof(TInterface);
}
