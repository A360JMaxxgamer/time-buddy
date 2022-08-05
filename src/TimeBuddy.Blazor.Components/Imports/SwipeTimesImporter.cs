using System.Xml.Serialization;
using Fluxor;
using TimeBuddy.Blazor.Components.Store.ProjectListUseCase;

namespace TimeBuddy.Blazor.Components.Imports;

public class SwipeTimesImporter : IImporter
{
    private readonly IApiClient _apiClient;
    private readonly IDispatcher _dispatcher;

    public SwipeTimesImporter(IApiClient apiClient, IDispatcher dispatcher)
    {
        _apiClient = apiClient;
        _dispatcher = dispatcher;
    }
    
    /// <exception cref="ArgumentNullException"></exception>
    /// <inheritdoc />
    public async Task ImportAsync(Stream data)
    {
        using var memoryStream = new MemoryStream();
        await data.CopyToAsync(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);
        var xmlSerializer = new XmlSerializer(typeof(SwipeTimes));
        if (xmlSerializer.Deserialize(memoryStream) is not SwipeTimes swipeTimes) 
            throw new ArgumentNullException(nameof(swipeTimes));

        var groupedByProject = swipeTimes
            .Records
            .GroupBy(rec => rec.Project);

        foreach (var projectGroup in groupedByProject)
        {
            var project = new CreateProjectInput
            {
                ProjectName = projectGroup.Key,
                TimeFrameInputs = projectGroup
                    .Select(rec => new TimeFrameInput
                    {
                        StartDate = rec.Start,
                        Duration = rec.End - rec.Start
                    })
                    .ToList(),
            };
            await _apiClient.CreateProject.ExecuteAsync(project);
        }
        _dispatcher.Dispatch(new FetchProjectsAction());
    }
}

[XmlRoot("swipetimes")]
public record SwipeTimes
{
    [XmlElement("record")]
    public Record[] Records { get; set; } = Array.Empty<Record>();
}

public record Record
{
    [XmlElement("project")] public string Project { get; set; } = "Unknown";

    [XmlAttribute("start")] public string StartString { get; set; } = string.Empty;

    [XmlIgnore] public DateTime Start => DateTime.Parse(StartString);

    [XmlAttribute("end")] public string EndString { get; set; } = string.Empty;
    
    [XmlIgnore] public DateTime End => DateTime.Parse(EndString);
}