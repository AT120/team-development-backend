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
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("There is no teacher with this ID!");
            }

        }
    }
}
