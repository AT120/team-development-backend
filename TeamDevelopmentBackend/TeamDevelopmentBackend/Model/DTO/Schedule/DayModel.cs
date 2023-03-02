using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace TeamDevelopmentBackend.Model.DTO.Schedule;

public class DayModel
{
    [JsonPropertyName("lessons")]
    public List<LessonModel> Lessons { get; set; }

    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    [JsonPropertyName("dayOfTheWeek")]
    public int DayOfTheWeek { get; set; }

    public DayModel(IGrouping<int, LessonDbModel> lessons, DateOnly date) 
    {
        this.Date = date;
        this.DayOfTheWeek = lessons.Key;
        this.Lessons = new List<LessonModel>();
        foreach (var lesson in lessons)
        {
            this.Lessons.Add(new LessonModel(lesson));
        }
    }
}