using System.Reactive.Disposables;
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
            this.BindValidation(ViewModel, vm => vm.Name, v => v._productName.ValidationError)
                .DisposeWith(disposables);

            this.BindValidation(ViewModel, vm => vm.Description, v => v._productDescription.ValidationError)
                .DisposeWith(disposables);

            this.BindValidation(ViewModel, vm => vm.ExpirationDate, v => v._expirationDate.ValidationError)
                .DisposeWith(disposables);
        });
    }
}