using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ReactiveUI;
using ReactiveUI.Blazor;

namespace ReactiveBlazor.Client.ReactiveUI;

public class ActivatableComponent<T> : ReactiveComponentBase<T>, ICanRender where T : class, INotifyPropertyChanged
{
    private bool _disposedValue;
    private readonly Subject<Unit> _renderedSubject = new();
    private readonly Subject<Unit> _disposedSubject = new();

    public ActivatableComponent()
    {
        Console.WriteLine("View Constructed");
        this.WhenActivated(disposables =>
        {
            Console.WriteLine("View Activated");
            //this call is required in order for the view model to be activated
        });
    }

    public IObservable<Unit> Rendered => _renderedSubject.AsObservable();
    public IObservable<Unit> Disposed => _disposedSubject.AsObservable();

    protected override void OnParametersSet()
    {
        Console.WriteLine("View Parameters Set");
        base.OnParametersSet();
    }

    protected override void OnInitialized()
    {
        Console.WriteLine("View Initialized");
        base.OnInitialized();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        Console.WriteLine("View Rendered");
        base.OnAfterRender(firstRender);
        _renderedSubject.OnNext(Unit.Default);
    }

    protected override void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _disposedSubject.OnNext(Unit.Default);
                _disposedSubject.Dispose();
                _renderedSubject.Dispose();
            }

            _disposedValue = true;
        }

        base.Dispose(disposing);
    }
}
