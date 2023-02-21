using TeamDevelopmentBackend.Model.DTO;

namespace TeamDevelopmentBackend.Services
{
    public interface ILessonService
    {
        public Task AddLesson(LessonDTOModel model);
        public Task EditLesson(Guid lessonId, LessonDTOModel model);
        public Task DeleteLesson(Guid lessonId);
    }
}
