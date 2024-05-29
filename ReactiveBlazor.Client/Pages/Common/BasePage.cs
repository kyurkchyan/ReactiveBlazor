using System.Reactive;
using System.Reactive.Disposables;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ReactiveBlazor.Client.Controls;
using ReactiveBlazor.Client.ReactiveUI;
using ReactiveBlazor.ViewModels.Common.ViewModels;
using ReactiveUI;
using Severity = MudBlazor.Severity;

namespace ReactiveBlazor.Client.Pages.Common;

public class BasePage<TViewModel> : ActivatableInjectableComponent<TViewModel> where TViewModel : class, INavigationViewModel
{
    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    public BasePage()
    {
        this.WhenActivated(disposables =>
        {
            this.BindInteraction(ViewModel,
                                 vm => vm.ShowDialog,
                                 dialog => InvokeAsync(async () =>
                                 {
                                     var dialogViewModel = dialog.Input;
                                     bool? result = true;

                                     if (!string.IsNullOrWhiteSpace(dialogViewModel.CancelButtonText))
                                     {
                                         result = await DialogService.ShowMessageBox(dialogViewModel.Title,
                                                                                     dialogViewModel.Description,
                                                                                     dialogViewModel.ConfirmButtonText,
                                                                                     dialogViewModel.CancelButtonText);
                                     }
                                     else
                                     {
                                         await DialogService.ShowMessageBox(dialogViewModel.Title,
                                                                            dialogViewModel.Description,
                                                                            dialogViewModel.ConfirmButtonText);
                                     }

                                     dialog.SetOutput(result ?? false);
                                 }))
                .DisposeWith(disposables);

            this.BindInteraction(ViewModel,
                                 vm => vm.ShowSnackbar,
                                 snackbar => InvokeAsync(() =>
                                 {
                                     Snackbar.Add(snackbar.Input.Message, GetSeverity(snackbar.Input.Severity));
                                     snackbar.SetOutput(Unit.Default);
                                 }))
                .DisposeWith(disposables);

            this.BindInteraction(ViewModel,
                                 vm => vm.ShowAsyncConfirmDialog,
                                 async context =>
                                 {
                                     var parameters = new DialogParameters<AsyncConfirmDialog> { { d => d.Dialog, context.Input } };
                                     var dialog = await DialogService.ShowAsync<AsyncConfirmDialog>(context.Input.Title, parameters);
                                     var dialogResult = await dialog.Result;
                                     var didConfirm = dialogResult.Data as bool? ?? false;
                                     context.SetOutput(didConfirm);
                                 })
                .DisposeWith(disposables);
        });
    }

    private static Severity GetSeverity(ViewModels.Common.ViewModels.Severity inputSeverity)
    {
        return inputSeverity switch
        {
            ViewModels.Common.ViewModels.Severity.Info => Severity.Info,
            ViewModels.Common.ViewModels.Severity.Success => Severity.Success,
            ViewModels.Common.ViewModels.Severity.Warning => Severity.Warning,
            ViewModels.Common.ViewModels.Severity.Error => Severity.Error,
            ViewModels.Common.ViewModels.Severity.Normal => Severity.Normal,
            _ => throw new ArgumentOutOfRangeException(nameof(inputSeverity), inputSeverity, null)
        };
    }
}
