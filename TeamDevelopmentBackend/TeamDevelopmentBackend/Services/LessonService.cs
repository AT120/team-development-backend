using System;
using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO;

namespace TeamDevelopmentBackend.Services
{
    public class LessonService : ILessonService
    {
        private DefaultDBContext _dbContext;

        public LessonService(DefaultDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddLesson(LessonDTOModel model)
        {
            try
            {
                _dbContext.Lessons.Add(new LessonDbModel
                {
                    Id = new Guid(),
                    TimeSlot = model.TimeSlot,
                    Date = DateOnly.FromDateTime(model.Date),
                    TeacherId= model.TeacherId,
                    RoomId= model.RoomId,
                    GroupId= model.GroupId,
                    SubjectId= model.SubjectId
                });
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Wrong data in model!");
            }
        }

        public async Task DeleteLesson(Guid lessonId)
        {
            try
            {
                var lesson = _dbContext.Lessons.FirstOrDefault(x => x.Id == lessonId);
                _dbContext.Lessons.Remove(lesson);
                await _dbContext.SaveChangesAsync();
            }
            catch 
            {
                throw new Exception("There is no lesson with this ID!");
            }
        }

        public async Task EditLesson(Guid lessonId, LessonDTOModel model)
        {
            try
            {
                var lesson = _dbContext.Lessons.FirstOrDefault(x => x.Id == lessonId);
                
            }
            catch 
            {

            } 
        }
    }
}
