using System.Diagnostics;
using System.Reactive.Subjects;
using ReactiveBlazor.ViewModels.Common.Extensions;

namespace ReactiveBlazor.ViewModels.Common.ViewModels.Implementation;

public abstract class BaseDisposableViewModel : ReactiveObject, IDisposableViewModel
{
    private readonly Subject<Unit> _disposeSignal = new();
    public CompositeDisposable Disposables { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; } = ReactiveCommand.Create(() => { })!;

    private string LogEntryName => GetType().FullName!;

    protected BaseDisposableViewModel(CompositeDisposable? parentDisposables = null)
    {
        _disposeSignal.InvokeCommand(CancelCommand);
        Disposables = new CompositeDisposable();

        // If a parent disposables is provided, dispose this view model with the parent disposables
        // This is useful when a view model is created by another view model and the parent view model is disposed
        if (parentDisposables is not null)
        {
            Disposables.DisposeWith(parentDisposables);
        }

        Disposable.Create(() =>
        {
            // Deactivate the view model before disposing it
            Activator.Deactivate(true);
            // Remove the view model disposables from the parent disposables if it was provided
            // This may be necessary if the view model is disposed before the parent view model
            parentDisposables?.Remove(Disposables);
            // Signal the view model disposal
            _disposeSignal.OnNext(Unit.Default);
        }).DisposeWith(Disposables);

        Debug.WriteLine($"_____________________ViewModel Lifecycle: Creating ViewModel {LogEntryName}_____________________");
        Disposables.LogDisposal($"_____________________ViewModel Lifecycle: Disposing ViewModel {LogEntryName}_____________________");

        this.WhenActivated(disposables =>
        {
            Debug.WriteLine($"_____________________ViewModel Lifecycle: ViewModel {LogEntryName} activated_____________________");
            disposables.LogDisposal($"_____________________ViewModel Lifecycle: ViewModel {LogEntryName} deactivated_____________________");
        });
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Disposables.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public abstract Type Interface { get; }
    public ViewModelActivator Activator { get; } = new();
}
