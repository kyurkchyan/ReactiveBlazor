using System.ComponentModel;
using ReactiveUI.Blazor;

namespace ReactiveBlazor.Client.ReactiveUI;

public class ReactiveComponent<T> : ReactiveComponentBase<T> where T : class, INotifyPropertyChanged
{
}
