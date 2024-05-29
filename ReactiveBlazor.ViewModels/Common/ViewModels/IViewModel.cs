namespace ReactiveBlazor.ViewModels.Common.ViewModels;

public interface IViewModel : IReactiveObject
{
    public Type Interface { get; }
}
