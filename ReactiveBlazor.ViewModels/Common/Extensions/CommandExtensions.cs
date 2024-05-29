using System.Reactive.Threading.Tasks;
using ReactiveBlazor.ViewModels.Common.ViewModels;

namespace ReactiveBlazor.ViewModels.Common.Extensions;

public static class CommandExtensions
{
    public static IDisposable LogCommandErrors(this ILogger logger, params IHandleObservableErrors[] items)
    {
        return new CompositeDisposable(items.Select(item => item.ThrownExceptions
                                                                .Subscribe(error => logger.LogError(error, "An error occurred while executing a command"))));
    }

    public static Task WaitForCompletionAsync(this IReactiveCommand command)
    {
        return command.IsExecuting
                      .Where(isExecuting => !isExecuting)
                      .FirstAsync()
                      .ToTask();
    }

    public static IObservable<ErrorViewModel?> TrackErrors<TParam, TResult>(this ReactiveCommand<TParam, TResult> command, string message, Func<object?>? commandParameterFactory = null)
        => command.TrackErrors(_ => message, commandParameterFactory);

    public static IObservable<ErrorViewModel?> TrackErrors<TParam, TResult>(this ReactiveCommand<TParam, TResult> command, Func<Exception, string> messageFactory, Func<object?>? commandParameterFactory = null)
        => command
           .ThrownExceptions
           .Select(ex => new ErrorViewModel(command,
                                            commandParameterFactory?.Invoke(),
                                            ex,
                                            messageFactory(ex)))
           // Clear the error when the command starts executing
           .Merge(command.IsExecuting
                         .Where(isExecuting => isExecuting)
                         .Select(_ => null as ErrorViewModel))
           .Publish()
           .RefCount();
}
