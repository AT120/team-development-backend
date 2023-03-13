using TeamDevelopmentBackend.Model.DTO;

namespace TeamDevelopmentBackend.Services.Interfaces
{
    public interface ILessonService
    {
        public Task AddLesson(LessonDTOModel model);
        public Task EditLesson(Guid lessonId, LessonEditDTOModel model);
        public Task DeleteLesson(Guid lessonId);
    }
}
