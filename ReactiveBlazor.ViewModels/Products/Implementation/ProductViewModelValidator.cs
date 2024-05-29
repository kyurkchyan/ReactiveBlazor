using System.Reactive.Concurrency;
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
    }
}