using ReactiveBlazor.ViewModels.Common.ViewModels.Implementation;
using ReactiveUI.Fody.Helpers;

namespace ReactiveBlazor.ViewModels.Products.Implementation;

internal class ProductViewModel : BaseValidatableNavigationViewModel<IProductViewModel>, IProductViewModel
{
    public ProductViewModel()
    {
        Validator = new ProductViewModelValidator(this);
        Name = "Super cool product";
        Description = "This is a super cool product";
    }

    [Reactive]
    public string? Name { get; set; }

    [Reactive]
    public string? Description { get; set; }

    [Reactive]
    public DateTime? ExpirationDate { get; set; }

    [Reactive]
    public ProductCategory? Category { get; set; }

    public IEnumerable<ProductCategory> Categories { get; } = Enum.GetValues<ProductCategory>();
}
