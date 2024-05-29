using FluentValidation;
using ReactiveBlazor.ViewModels.Common.Validation;

namespace ReactiveBlazor.ViewModels.Products.Implementation;

internal class ProductViewModelValidator : AbstractViewModelValidator<IProductViewModel>
{
    public ProductViewModelValidator(IProductViewModel viewModel) : base(viewModel)
    {
        RuleFor(vm => vm.Name,
            builder =>
            {
                builder
                    .NotEmpty()
                    .WithMessage("You must specify a valid name")
                    .MaximumLength(10)
                    .WithMessage("Product name must be less than 10 characters");
            });

        RuleFor(vm => vm.Description,
            builder =>
            {
                builder
                    .NotEmpty()
                    .WithMessage("You must specify a valid description")
                    .MaximumLength(20)
                    .WithMessage("Product description must be less than 20 characters");
            });

        RuleFor(vm => vm.ExpirationDate,
            builder =>
            {
                builder
                    .NotNull()
                    .WithMessage("You must specify a valid expiration date")
                    .GreaterThan(DateTime.Now)
                    .WithMessage("Expiration date must be in the future");
            });

        RuleFor(vm => vm.Category,
            builder =>
            {
                builder
                    .NotNull()
                    .WithMessage("You must specify a valid category")
                    .Must(category => category != ProductCategory.Antiques)
                    .WithMessage("Sorry, we don't accept antiques at the moment");
            });
    }
}