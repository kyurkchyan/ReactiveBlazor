using ReactiveUI;
using ReactiveUI.Validation.Formatters.Abstractions;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using Splat.Microsoft.Extensions.Logging;
using Splat.ModeDetection;

namespace ReactiveBlazor.Client.ReactiveUI;

public static class Startup
{
    public static IServiceCollection ConfigureReactiveUI(this IServiceCollection services)
    {
        ModeDetector.OverrideModeDetector(Mode.Run);
        services.UseMicrosoftDependencyResolver();
        var resolver = Locator.CurrentMutable;
        resolver.UseMicrosoftExtensionsLoggingWithWrappingFullLogger(services.BuildServiceProvider().GetRequiredService<ILoggerFactory>());
        resolver.InitializeSplat();
        resolver.InitializeReactiveUI();

        return services
               .AddTransient<IActivationForViewFetcher, ActivationAfterRenderForViewFetcher>()
               .AddTransient<IValidationTextFormatter<string>, FirstErrorValidationTextFormatter>();
    }
}
