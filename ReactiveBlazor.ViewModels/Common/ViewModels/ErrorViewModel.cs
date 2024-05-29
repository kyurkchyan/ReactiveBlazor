using System.Windows.Input;

namespace ReactiveBlazor.ViewModels.Common.ViewModels;

public sealed class ErrorViewModel(ICommand command,
                                   object? commandParameter = null,
                                   Exception? exception = null,
                                   string? message = null) : ReactiveObject
{
    public ICommand Command { get; } = command;
    public object? CommandParameter { get; } = commandParameter;
    public Exception? Exception { get; } = exception;
    public string? Message { get; } = message;
}
