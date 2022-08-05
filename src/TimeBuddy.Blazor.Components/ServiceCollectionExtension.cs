using Fluxor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeBuddy.Blazor.Components.Imports;
using TimeBuddy.Blazor.Components.Services;

namespace TimeBuddy.Blazor.Components;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddTimeBuddyServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddFluxor(options => options.ScanAssemblies(typeof(ServiceCollectionExtension).Assembly));
        serviceCollection.AddScoped<ITimerService, TimerService>();
        serviceCollection.AddScoped<IImporter, SwipeTimesImporter>();
        serviceCollection.AddApiClient()
            .ConfigureHttpClient((provider, client) =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var apiEndpoint = config.GetValue<string>("Api");
                client.BaseAddress = new Uri(apiEndpoint);
            });
        
        return serviceCollection;
    }
}