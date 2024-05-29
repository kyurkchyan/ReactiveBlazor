namespace ReactiveBlazor.ViewModels.Common.ViewModels;

public interface IDisposableViewModel : IViewModel, IActivatableViewModel, IDisposable
{
    public CompositeDisposable Disposables { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }
}
