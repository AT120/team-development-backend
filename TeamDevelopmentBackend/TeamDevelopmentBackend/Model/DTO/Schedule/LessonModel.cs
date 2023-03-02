using System.Text.Json.Serialization;

namespace TeamDevelopmentBackend.Model.DTO.Schedule;

public class LessonModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("timeslot")]
    public int Timeslot { get; set; }
    
    [JsonPropertyName("group")]
    public GroupDbModel Group { get; set; }
    
    [JsonPropertyName("teacher")]
    public TeacherDbModel Teacher { get; set; }

    [JsonPropertyName("room")]
    public RoomDTOModel Room { get; set; }
    
    [JsonPropertyName("building")]
    public BuildingDTOModel Building { get; set; }

    [JsonPropertyName("subject")]
    public SubjectDbModel Subject { get; set; }
    
    [JsonPropertyName("startDate")]
    public DateOnly StartDate { get; set; }

    [JsonPropertyName("endDate")]
    public DateOnly? EndDate { get; set; }

    public LessonModel(LessonDbModel lesson)
    {
        Id = lesson.Id;
        Timeslot = lesson.TimeSlot;
        Group = lesson.Group;
        Teacher = lesson.Teacher;
        Room = new RoomDTOModel(lesson.Room);
        Building = new BuildingDTOModel(lesson.Room.Building);
        Subject = lesson.Subject;
        StartDate = lesson.StartDate;
        EndDate = lesson.EndDate; //TODO: maybe if (lesson.EndDate == defaultEndDate) : null ?
    }
}