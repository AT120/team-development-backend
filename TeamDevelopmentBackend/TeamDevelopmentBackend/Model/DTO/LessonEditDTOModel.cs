namespace TeamDevelopmentBackend.Model.DTO
{
    public class LessonEditDTOModel
    {
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public int? TimeSlot { get; set; }
            public Guid? TeacherId { get; set; }
            public Guid? RoomId { get; set; }
            public Guid? GroupId { get; set; }
            public Guid? SubjectId { get; set; }
            public Guid? BuildingId { get; set; }
    }
}
