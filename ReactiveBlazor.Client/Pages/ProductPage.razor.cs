using System.Reactive.Disposables;
using ReactiveBlazor.Client.ReactiveUI.Forms;
using ReactiveBlazor.ViewModels;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;

namespace ReactiveBlazor.Client.Pages;

public partial class ProductPage
{
    private ReactiveTextField<string> _productDescription = null!;

    public ProductPage()
    {
        this.WhenActivated(disposables =>
        {
            this.BindValidation(ViewModel, vm => vm.Name, v => v.ProductNameValidation)
                .DisposeWith(disposables);

            this.BindValidation(ViewModel, vm => vm.Description, v => v._productDescription.ValidationError)
                .DisposeWith(disposables);
        });
    }

    private string? _productNameValidation;

    public string? ProductNameValidation
    {
        get => _productNameValidation;
        set
        {
            if (_productNameValidation == value) return;
            _productNameValidation = value;
            StateHasChanged();
        }
    }
}