using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Services.Interfaces;

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
            var teacher = await _dbContext.Teachers.FindAsync(Id);
            if (teacher != null) {                           
                var lessons2 = _dbContext.Lessons.Where(x => x.TeacherId == Id && x.StartDate < DateOnly.FromDateTime(DateTime.Now)).ToList();
                if (lessons2.Count != 0)
                {
                    throw new InvalidOperationException("There is lesson in the past with this Teacher!");
                }
                else   
                {
                    var user =  _dbContext.Users.FirstOrDefault(x=>x.DefaultFilterId==Id);
                    _dbContext.Teachers.Remove(teacher);
                    if (user != null)
                    {
                        user.Role = Role.Student;
                        user.DefaultFilterId = null;
                    }
                    await _dbContext.SaveChangesAsync();
                }              
            }
            else
            {
                throw new ArgumentException("There is no teacher with this ID!");
            }

        }
    }
}
