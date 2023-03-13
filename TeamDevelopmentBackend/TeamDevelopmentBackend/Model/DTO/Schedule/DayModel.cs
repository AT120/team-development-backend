using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace TeamDevelopmentBackend.Model.DTO.Schedule;

public class DayModel
{
    private readonly TimeOnly zeroTime = new();
    public List<LessonModel> Lessons { get; set; }
    public DateTime Date { get; set; }
    public int DayOfTheWeek { get; set; }

    public DayModel(IGrouping<int, LessonDbModel> lessons, DateOnly date) 
    {
        this.Date = date.ToDateTime(zeroTime);
        this.DayOfTheWeek = lessons.Key;
        this.Lessons = new List<LessonModel>();
        foreach (var lesson in lessons)
        {
            this.Lessons.Add(new LessonModel(lesson));
        }
    }
}