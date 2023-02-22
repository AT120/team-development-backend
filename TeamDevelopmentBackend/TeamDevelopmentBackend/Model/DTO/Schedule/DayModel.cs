using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace TeamDevelopmentBackend.Model.DTO.Schedule;

public class DayModel
{
    public List<LessonDTOModel> lessons { get; set; }
    public DateOnly date { get; set; }
    public int dayOfTheWeek { get; set; }

    public DayModel(IGrouping<DateOnly, LessonDbModel> lessons) 
    {
        date = lessons.Key;
        dayOfTheWeek = (int)date.DayOfWeek; //TODO: воскресенье = 0
        this.lessons = new List<LessonDTOModel>();
        foreach (var lesson in lessons)
        {
            this.lessons.Add(new LessonDTOModel(lesson));
        }
    }
}