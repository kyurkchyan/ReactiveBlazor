using MudBlazor;
using MudBlazor.Services;

namespace ReactiveBlazor.Client.DependencyInjection;

public static class MudBlazor
{
    public static IServiceCollection ConfigureMudBlazor(this IServiceCollection services)
        => services
           .AddMudServices(config =>
           {
               config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
               config.SnackbarConfiguration.MaxDisplayedSnackbars = 10;
               config.SnackbarConfiguration.PreventDuplicates = false;
               config.SnackbarConfiguration.NewestOnTop = false;
               config.SnackbarConfiguration.ShowCloseIcon = true;
               config.SnackbarConfiguration.VisibleStateDuration = 5000;
               config.SnackbarConfiguration.HideTransitionDuration = 500;
               config.SnackbarConfiguration.ShowTransitionDuration = 500;
               config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
           });
}
