using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveBlazor.Client.ReactiveUI.Forms;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace ReactiveBlazor.Client.Pages;

public partial class ProductPage
{
    private ReactiveTextField<string> _productName = null!;
    private ReactiveTextField<string> _productDescription = null!;
    private ReactiveDatePicker _expirationDate = null!;

    public ProductPage()
    {
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(v => v.ViewModel)
                .WhereNotNull()
                .Select(vm => vm.ValidationContext.ValidationStatusChange)
                .Switch()
                .DistinctUntilChanged()
                .Subscribe(_ => InvokeAsync(StateHasChanged))
                .DisposeWith(disposables);

            this.BindValidation(ViewModel, vm => vm.Name, v => v._productName.ValidationError)
                .DisposeWith(disposables);

            this.BindValidation(ViewModel, vm => vm.Description, v => v._productDescription.ValidationError)
                .DisposeWith(disposables);

            this.BindValidation(ViewModel, vm => vm.ExpirationDate, v => v._expirationDate.ValidationError)
                .DisposeWith(disposables);
        });
    }

    private void OnClearClicked()
    {
        if (ViewModel == null)
        {
            return;
        }

        ViewModel.Name = null;
        ViewModel.Description = null;
        ViewModel.ExpirationDate = null;
    }
}