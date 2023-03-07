using Microsoft.EntityFrameworkCore;
using TeamDevelopmentBackend.Model;

namespace TeamDevelopmentBackend.Services
{
    public class TeacherService : ITeacherService
    {
        private DefaultDBContext _dbContext;
        public TeacherService(DefaultDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public TeacherDbModel[] GetTeachers()
        {
            return _dbContext.Teachers.ToArray();
        }
        public async Task AddTeacher(string name)
        {
                _dbContext.Teachers.Add(new TeacherDbModel { Id = new Guid(), Name = name });
                await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTeacher(Guid Id)
        {
            var teacher = _dbContext.Teachers.FirstOrDefault(x => x.Id == Id);
            try
            {
                _dbContext.Teachers.Remove(teacher);
                var user = _dbContext.Users.FirstOrDefault(x=> x.Id == Id);
                var lessons = _dbContext.Lessons.Where(x => x.TeacherId==Id && x.StartDate>=DateOnly.FromDateTime(DateTime.Now)).ToList();
                lessons.ForEach(x => _dbContext.Remove(x));
                if (user != null)
                {
                    user.Role = Role.Student;
                }
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("There is no teacher with this ID!");
            }

        }
    }
}
