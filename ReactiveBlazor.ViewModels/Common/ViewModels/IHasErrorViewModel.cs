namespace ReactiveBlazor.ViewModels.Common.ViewModels;

public interface IHasErrorViewModel : IViewModel
{
    public ErrorViewModel? Error { get; }
}
