using Microsoft.Extensions.DependencyInjection;
using ReactiveBlazor.ViewModels.Products;
using ReactiveBlazor.ViewModels.Products.Implementation;

namespace ReactiveBlazor.ViewModels;

public static class Startup
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
        => services
            .AddTransient<IProductViewModel, ProductViewModel>();
}