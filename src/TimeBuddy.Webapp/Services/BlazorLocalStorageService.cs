using System.ComponentModel.DataAnnotations;
using IndexedDB.Blazor;
using Microsoft.JSInterop;
using TimeBuddy.Blazor.Components.Services;

namespace TimeBuddy.Webapp.Services;

internal class BlazorLocalStorageService : ILocalStorageService
{
    private readonly IIndexedDbFactory _indexedDbFactory;

    public BlazorLocalStorageService(IIndexedDbFactory indexedDbFactory)
    {
        _indexedDbFactory = indexedDbFactory;
    }

    /// <inheritdoc />
    public async Task SaveAsync<T>(string key, T value)
    {
        using var db = await _indexedDbFactory.Create<GenericIndexedDb<T>>();
        var toSave = db.Values.SingleOrDefault(val => val.Id == key);
        if (toSave is null)
        {
            toSave = new IndexedEntry<T>();
            db.Values.Add(toSave);
        }
        toSave.Value = value;
        await db.SaveChanges();
    }

    /// <inheritdoc />
    public async Task<T> LoadAsync<T>(string key)
    {
        using var db = await _indexedDbFactory.Create<GenericIndexedDb<T>>();
        var result = db.Values.SingleOrDefault(val => val.Id == key);
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(result.Value);
        return result.Value;
    }
}

internal class GenericIndexedDb<T> : IndexedDb
{
#pragma warning disable CS8618
    public GenericIndexedDb(IJSRuntime jSRuntime, string name, int version) : base(jSRuntime, name, version) { }
#pragma warning restore CS8618

    public IndexedSet<IndexedEntry<T>> Values { get; set; } 
}

internal class IndexedEntry<T>
{
    [Key] public string Id { get; set; } = "Unknown";

    public T? Value { get; set; }
}