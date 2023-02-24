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
            var lesson = new LessonDbModel
            {
                Id = new Guid(),
                TimeSlot = model.TimeSlot,
                StartDate = DateOnly.FromDateTime(model.Date),
                TeacherId = model.TeacherId,
                RoomId = model.RoomId,
                GroupId = model.GroupId,
                SubjectId = model.SubjectId,
                EndDate = model.IsOneTime == true ? DateOnly.FromDateTime(model.Date).AddDays(1) : DateOnly.MaxValue,
                WeekDay = (int)DateOnly.FromDateTime(model.Date).DayOfWeek
            };
            if (await _dbContext.CheckIfCanBeAddedInDatabase(lesson))
            {
                await _dbContext.Lessons.AddAsync(lesson);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("There is already lesson on this slot!");
            }
        }

        public async Task DeleteLesson(Guid lessonId, bool isOneTime, DateTime date)
        {
            try
            {
                var lesson = _dbContext.Lessons.FirstOrDefault(x => x.Id == lessonId);

                if (isOneTime)
                {
                    lesson.EndDate = DateOnly.FromDateTime(date.AddDays(-1));
                   await _dbContext.Lessons.AddAsync(new LessonDbModel
                    {
                        Id = new Guid(),
                        GroupId = lesson.GroupId,
                        SubjectId = lesson.SubjectId,
                        RoomId = lesson.RoomId,
                        WeekDay = lesson.WeekDay,
                        TimeSlot = lesson.TimeSlot,
                        TeacherId = lesson.TeacherId,
                        StartDate = DateOnly.FromDateTime(date.AddDays(7)),
                        EndDate = lesson.EndDate
                    });
                    await _dbContext.SaveChangesAsync();
                }
                else
                {

                    lesson.EndDate = DateOnly.FromDateTime(date.AddDays(-1));
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch
            {
                throw new Exception("There is no lesson with this ID!");
            }

        }

        public async Task EditLesson(Guid lessonId, LessonDTOModel model, DateTime date)
        {
            try
            {
                var lesson = _dbContext.Lessons.FirstOrDefault(x => x.Id == lessonId);
                lesson.EndDate = DateOnly.FromDateTime(date);
                var newLesson = new LessonDbModel
                {
                    Id = new Guid(),
                    GroupId = model.GroupId,
                    SubjectId = model.SubjectId,
                    RoomId = model.RoomId,
                    WeekDay = (int)DateOnly.FromDateTime(model.Date).DayOfWeek,
                    TimeSlot = model.TimeSlot,
                    TeacherId = model.TeacherId,
                    StartDate = DateOnly.FromDateTime(model.Date),
                    EndDate = model.IsOneTime == true ? DateOnly.FromDateTime(model.Date).AddDays(1) : DateOnly.MaxValue,
                };

                if(await _dbContext.CheckIfCanBeAddedInDatabase(newLesson))
                {
                    await _dbContext.AddAsync(newLesson);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("There is already lesson on this slot!");
                }
            }
            catch 
            {
                throw new Exception("There is no lesson with this ID!");
            } 
        }
    }
}
