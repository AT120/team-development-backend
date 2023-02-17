using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamDevelopmentBackend.Model
{
    public class LessonDbModel
    {
        public Guid Id { get; set; }
        public int TimeSlot { get; set; }
      
        public Guid RoomId { get; set; }
        public RoomDbModel Room { get; set; }

        public SubjectDbModel Subject { get; set; }
      
        public Guid TeacherId { get; set; }
        public TeacherDbModel Teacher { get; set;}
   
        public Guid GroupId { get; set; }
        public GroupDbModel Group { get; set; }

        public DateOnly Date { get; set; } 

    }
}
