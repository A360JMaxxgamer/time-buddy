using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using TimeBuddy.Core.Services;

namespace TimeBuddy.Blazor.Services;

internal class BlazorLocalStorageService : ILocalStorageService
{
    private readonly ProtectedLocalStorage _protectedLocalStorage;

    public BlazorLocalStorageService(ProtectedLocalStorage protectedLocalStorage)
    {
        _protectedLocalStorage = protectedLocalStorage;
    }

    /// <inheritdoc />
    public async Task SaveAsync<T>(string key, T value)
        => await _protectedLocalStorage.SetAsync(key, value ?? throw new ArgumentNullException(nameof(value)));

    /// <inheritdoc />
    public async Task<T> LoadAsync<T>(string key)
    {
        var result = await _protectedLocalStorage.GetAsync<T>(key);
        ArgumentNullException.ThrowIfNull(result.Value);
        return result.Value;
    }
}