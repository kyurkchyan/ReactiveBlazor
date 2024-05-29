using Microsoft.Extensions.DependencyInjection;

namespace ReactiveBlazor.ViewModels;

public static class Startup
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
        => services
            .AddTransient<ProductViewModel>();
}