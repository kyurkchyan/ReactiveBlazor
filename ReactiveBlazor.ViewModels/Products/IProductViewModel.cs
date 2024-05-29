using ReactiveBlazor.ViewModels.Common.Validation;
using ReactiveBlazor.ViewModels.Common.ViewModels;

namespace ReactiveBlazor.ViewModels.Products;

public interface IProductViewModel : INavigationViewModel, IValidatableViewModel
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public ProductCategory? Category { get; set; }

    public IEnumerable<ProductCategory> Categories { get; }
}
