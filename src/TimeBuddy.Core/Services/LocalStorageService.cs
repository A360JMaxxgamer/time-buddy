using System.Text.Json;

namespace TimeBuddy.Core.Services;

internal class LocalStorageService : ILocalStorageService
{
    private readonly DirectoryInfo _folder;

    public LocalStorageService(DirectoryInfo folder)
    {
        _folder = folder;
    }

    /// <inheritdoc />
    public Task SaveAsync<T>(string key, T value)
    {
        var serialized = JsonSerializer.Serialize(value);
        File.WriteAllText(GetFilePath(key), serialized);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task<T> LoadAsync<T>(string key)
    {
        var result = JsonSerializer.Deserialize<T>(File.ReadAllText(GetFilePath(key)));
        ArgumentNullException.ThrowIfNull(result);
        return Task.FromResult(result);
    }

    private string GetFilePath(string key) => Path.Combine(_folder.FullName, key);
}