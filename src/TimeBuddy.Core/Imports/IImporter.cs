namespace TimeBuddy.Core.Imports;

public interface IImporter
{
    Task ImportAsync(Stream data);
}