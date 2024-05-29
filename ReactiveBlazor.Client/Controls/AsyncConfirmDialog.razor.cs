using System.Reactive.Disposables;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ReactiveBlazor.ViewModels.Common.Extensions;
using ReactiveBlazor.ViewModels.Common.ViewModels;
using ReactiveUI;

namespace ReactiveBlazor.Client.Controls;

public partial class AsyncConfirmDialog
{
    private readonly DialogOptions _dialogOptions = new()
    {
        FullWidth = true,
        DisableBackdropClick = true,
        CloseOnEscapeKey = false
    };

    private MudButton _confirmButton = null!;

    public AsyncConfirmDialog()
    {
        this.WhenActivated(disposables =>
        {
            this.BindCommand(ViewModel, vm => vm.ConfirmCommand, v => v._confirmButton)
                .DisposeWith(disposables);

            this.WhenAnyObservable(v => v.ViewModel!.ConfirmCommand.IsExecuting)
                .Subscribe(isExecuting => InvokeAsync(() => IsExecuting = isExecuting))
                .DisposeWith(disposables);

            this.WhenAnyObservable(v => v.ViewModel!.ConfirmCommand)
                .Subscribe(didSucceed => InvokeAsync(() =>
                {
                    if (didSucceed)
                    {
                        MudDialog.Close(DialogResult.Ok(true));
                    }
                }))
                .DisposeWith(disposables);

            this.WhenAnyValue(v => v.ViewModel!.ConfirmActionType)
                .Subscribe(actionType =>
                {
                    ConfirmIcon = actionType switch
                    {
                        DialogConfirmActionType.Add => Icons.Material.Outlined.Add,
                        DialogConfirmActionType.Delete => Icons.Material.Outlined.Delete,
                        DialogConfirmActionType.Save => Icons.Material.Outlined.Save,
                        _ => Icons.Material.Outlined.Check
                    };

                    ConfirmColor = actionType switch
                    {
                        DialogConfirmActionType.Delete => Color.Error,
                        _ => Color.Primary
                    };
                });

            // TODO: figure out a better way to refresh the UI sate
            this.WhenAnyObservable(v => v.ViewModel!.ConfirmCommand.CanExecute).ToUnit()
                .Subscribe(_ => InvokeAsync(StateHasChanged))
                .DisposeWith(disposables);
        });
    }

    [Parameter]
    public AsyncConfirmDialogViewModel? Dialog { get; set; }

    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = null!;

    private bool _isExecuting;

    private bool IsExecuting
    {
        get => _isExecuting;
        set
        {
            _isExecuting = value;
            StateHasChanged();
        }
    }

    private string? _confirmIcon;

    private string? ConfirmIcon
    {
        get => _confirmIcon;
        set
        {
            _confirmIcon = value;
            StateHasChanged();
        }
    }

    private Color _confirmColor;

    private Color ConfirmColor
    {
        get => _confirmColor;
        set
        {
            _confirmColor = value;
            StateHasChanged();
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        ViewModel = Dialog;
    }

    private void OnCancelClicked()
    {
        MudDialog.Close(DialogResult.Cancel());
    }
}
