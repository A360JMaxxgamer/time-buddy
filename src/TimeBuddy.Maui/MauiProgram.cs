using MudBlazor.Services;
using TimeBuddy.Core;

namespace TimeBuddy.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Services
            .AddLogging()
            .AddMudServices()
            .AddTimeBuddyServices()
            .AddLocalStorageService(_ => new DirectoryInfo($"{FileSystem.AppDataDirectory}"))
            .AddTimeBuddyContext($"{FileSystem.Current.AppDataDirectory}/timeBuddy.db")
            .AddStateManagement();

        
        return builder.Build();
    }
}