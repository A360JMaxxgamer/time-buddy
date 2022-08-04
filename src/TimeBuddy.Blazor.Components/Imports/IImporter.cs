namespace TimeBuddy.Blazor.Components.Imports;

public interface IImporter
{
    Task ImportAsync(Stream data);
}