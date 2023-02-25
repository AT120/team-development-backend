namespace TeamDevelopmentBackend.Model.DTO.Schedule;

public class ScheduleModel
{
    public List<DayModel> days { get; set; }

    public ScheduleModel(IEnumerable<IGrouping<int, LessonDbModel>> daysWithLessons, DateOnly monday) 
    {
        days = new List<DayModel>();
        foreach (var day in daysWithLessons)
        {
            DateOnly date = monday.AddDays(day.Key - 1);  //TODO: sunday 
            days.Add(new DayModel(day, date));
        }
    } 
}

