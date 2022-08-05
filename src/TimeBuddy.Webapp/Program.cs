using System.Diagnostics;
using IndexedDB.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TimeBuddy.Blazor.Components;
using TimeBuddy.Blazor.Components.Services;
using TimeBuddy.Webapp;
using TimeBuddy.Webapp.Services;

#if DEBUG
while (!Debugger.IsAttached)
{
    await Task.Delay(200);
}
#endif

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

builder.Services
    .AddLogging()
    .AddMudServices()
    .AddTimeBuddyServices()
    .AddSingleton<IIndexedDbFactory, IndexedDbFactory>()
    .AddScoped<ILocalStorageService, BlazorLocalStorageService>();

await builder.Build().RunAsync();