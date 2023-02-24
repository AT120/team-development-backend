namespace TeamDevelopmentBackend.Model.DTO
{
    public class LessonDTOModel
    {
        public DateTime Date { get; set; }
        public int TimeSlot { get; set; }
        public bool IsOneTime { get; set; }
        public Guid TeacherId { get; set; }
        public Guid RoomId { get; set; }
        public Guid GroupId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid BuildingId { get; set; }
    }
}
