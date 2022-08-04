using MudBlazor.Services;
using TimeBuddy.Blazor.Components.Services;
using TimeBuddy.Blazor.Services;
using TimeBuddy.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services
    .AddLogging()
    .AddMudServices()
    .AddTimeBuddyServices()
    .AddScoped<ILocalStorageService, BlazorLocalStorageService>()
    .AddStateManagement();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();