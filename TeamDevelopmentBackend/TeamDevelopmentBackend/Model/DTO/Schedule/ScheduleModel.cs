namespace TeamDevelopmentBackend.Model.DTO.Schedule;

public class ScheduleModel
{
    public List<DayModel> days { get; set; }

    public ScheduleModel(IEnumerable<IGrouping<int, LessonDbModel>> daysWithLessons) 
    {
        days = new List<DayModel>();
        foreach (var day in daysWithLessons)
        {
            days.Add(new DayModel(day));
        }
    } 
}

