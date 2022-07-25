using System.Xml.Serialization;
using Fluxor;
using TimeBuddy.Core.Contexts;
using TimeBuddy.Core.Models;
using TimeBuddy.Core.Store.ProjectListUseCase;

namespace TimeBuddy.Core.Imports;

public class SwipeTimesImporter : IImporter
{
    private readonly TimeBuddyContext _timeBuddyContext;
    private readonly IDispatcher _dispatcher;

    public SwipeTimesImporter(TimeBuddyContext timeBuddyContext, IDispatcher dispatcher)
    {
        _timeBuddyContext = timeBuddyContext;
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
            var project = new Project
            {
                Name = projectGroup.Key,
                CreatedAt = DateTime.UtcNow,
                TimeFrames = projectGroup
                    .Select(rec => new TimeFrame
                    {
                        StartDate = rec.Start,
                        Duration = rec.End - rec.Start
                    })
                    .ToList(),
                Settings = new ProjectSettings
                {
                    TargetHoursPerDay = 8
                }
            };
            _timeBuddyContext.Projects.Add(project);
            await _timeBuddyContext.SaveChangesAsync();
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

    [XmlAttribute("start")]
    public string StartString { get; set; }

    [XmlIgnore] public DateTime Start => DateTime.Parse(StartString);
    
    [XmlAttribute("end")]
    public string EndString { get; set; }
    
    [XmlIgnore] public DateTime End => DateTime.Parse(EndString);
}