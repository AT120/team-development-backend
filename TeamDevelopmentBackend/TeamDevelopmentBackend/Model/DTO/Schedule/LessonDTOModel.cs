namespace TeamDevelopmentBackend.Model.DTO.Schedule;

public class LessonDTOModel
{
    public Guid id { get; set; }
    public int timeslot { get; set; }
    public GroupDbModel group { get; set; }
    public TeacherDbModel teacher { get; set; }
    public RoomDTOModel room { get; set; }
    public BuildingDTOModel building { get; set; }
    public SubjectDbModel subject { get; set; }
    public DateOnly startDate { get; set; }
    public DateOnly? endDate { get; set; }

    public LessonDTOModel(LessonDbModel lesson)
    {
        id = lesson.Id;
        timeslot = lesson.TimeSlot;
        group = lesson.Group;
        teacher = lesson.Teacher;
        room = new RoomDTOModel(lesson.Room);
        building = new BuildingDTOModel(lesson.Room.Building);
        subject = lesson.Subject;
        startDate = lesson.StartDate;
        endDate = lesson.EndDate; //TODO: maybe if (lesson.EndDate == defaultEndDate) : null ?
    }
}