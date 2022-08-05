namespace TimeBuddy.Blazor.Components.Services;

public interface ILocalStorageService
{
    Task SaveAsync<T>(string key, T value);

    Task<T> LoadAsync<T>(string key);
}