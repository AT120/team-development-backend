using System.Text.Json.Serialization;

namespace TeamDevelopmentBackend.Model.DTO.Schedule;

public class LessonModel
{
    public Guid Id { get; set; }
    public int Timeslot { get; set; }
    public GroupDbModel Group { get; set; }
    public TeacherDbModel Teacher { get; set; }
    public RoomDTOModel Room { get; set; }
    public BuildingDTOModel Building { get; set; }
    public SubjectDbModel Subject { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public LessonModel(LessonDbModel lesson)
    {
        Id = lesson.Id;
        Timeslot = lesson.TimeSlot;
        Group = lesson.Group;
        Teacher = lesson.Teacher;
        Room = new RoomDTOModel(lesson.Room);
        Building = new BuildingDTOModel(lesson.Room.Building);
        Subject = lesson.Subject;
        StartDate = lesson.StartDate.ToDateTime(new TimeOnly());
        EndDate = lesson.EndDate.ToDateTime(new TimeOnly()); //TODO: maybe if (lesson.EndDate == defaultEndDate) : null ?
    }
}