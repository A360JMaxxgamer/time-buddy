namespace TimeBuddy.Core.Models;

public class TimeFrame
{
    public int Id { get; set; }

    public DateTime StartDate { get; set; }

    public TimeSpan Duration { get; set; }
}