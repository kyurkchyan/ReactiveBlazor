using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;

namespace ReactiveBlazor.ViewModels;

public class ProductViewModel : ReactiveObject, IValidatableViewModel
{
    public ProductViewModel()
    {
        Name = "Super cool product";
        Description = "This is a super cool product";
        this.ValidationRule(
            viewModel => viewModel.Name,
            name => !string.IsNullOrWhiteSpace(name),
            "You must specify a valid name");

        this.ValidationRule(
            viewModel => viewModel.Name,
            name => name?.Length <= 10,
            "Product name must be less than 10 characters");

        this.ValidationRule(
            viewModel => viewModel.Description,
            description => !string.IsNullOrWhiteSpace(description),
            "You must specify a valid description");

        this.ValidationRule(
            viewModel => viewModel.Description,
            description => description?.Length <= 20,
            "Product description must be less than 20 characters");
    }

    [Reactive] public string? Name { get; set; }

    [Reactive] public string? Description { get; set; }

    public IValidationContext ValidationContext { get; } = new ValidationContext();
}