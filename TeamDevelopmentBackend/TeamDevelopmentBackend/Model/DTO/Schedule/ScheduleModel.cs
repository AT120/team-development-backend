using System.Text.Json.Serialization;

namespace TeamDevelopmentBackend.Model.DTO.Schedule;

public class ScheduleModel
{
    [JsonPropertyName("days")]
    public List<DayModel> Days { get; set; }

    public ScheduleModel(IEnumerable<IGrouping<int, LessonDbModel>> daysWithLessons, DateOnly monday) 
    {
        Days = new List<DayModel>();
        foreach (var day in daysWithLessons)
        {
            DateOnly date = monday.AddDays(day.Key - 1);  //TODO: sunday 
            Days.Add(new DayModel(day, date));
        }
    } 
}

