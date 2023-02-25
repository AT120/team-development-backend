using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace TeamDevelopmentBackend.Model.DTO.Schedule;

public class DayModel
{
    public List<LessonDTOModel> lessons { get; set; }
    public DateOnly date { get; set; }
    public int dayOfTheWeek { get; set; }

    public DayModel(IGrouping<int, LessonDbModel> lessons, DateOnly date) 
    {
        this.date = date;
        this.dayOfTheWeek = lessons.Key;
        this.lessons = new List<LessonDTOModel>();
        foreach (var lesson in lessons)
        {
            this.lessons.Add(new LessonDTOModel(lesson));
        }
    }
}