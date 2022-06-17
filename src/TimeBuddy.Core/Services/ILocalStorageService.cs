namespace TimeBuddy.Core.Services;

public interface ILocalStorageService
{
    Task SaveAsync<T>(string key, T value);

    Task<T> LoadAsync<T>(string key);
}