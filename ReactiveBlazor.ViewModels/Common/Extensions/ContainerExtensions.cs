using Microsoft.Extensions.DependencyInjection;
using ReactiveBlazor.ViewModels.Common.ViewModels;

namespace ReactiveBlazor.ViewModels.Common.Extensions;

public static class ContainerExtensions
{
    public static IServiceCollection RegisterViewForViewModel<TViewModel, TView>(this IServiceCollection services, string? contract = null)
        where TView : class, IViewFor<TViewModel>
        where TViewModel : class, IReactiveObject
    {
        contract ??= typeof(TViewModel).FullName!;

        services.AddKeyedTransient(typeof(IViewFor),
                                   contract,
                                   typeof(TView));

        return services;
    }

    public static IViewFor ResolveViewForViewModel(this IServiceProvider container, IViewModel viewModel, string? contract = null)
    {
        contract ??= viewModel.Interface.FullName!;
        var view = container.GetRequiredKeyedService<IViewFor>(contract);
        view.ViewModel = viewModel;
        return view;
    }
}
