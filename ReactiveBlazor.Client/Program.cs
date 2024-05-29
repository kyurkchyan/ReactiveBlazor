using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ReactiveBlazor.Client.DependencyInjection;
using ReactiveBlazor.Client.ReactiveUI;
using ReactiveBlazor.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services
       .ConfigureMudBlazor()
       .ConfigureReactiveUI()
       .AddViewModels();
await builder.Build().RunAsync();
