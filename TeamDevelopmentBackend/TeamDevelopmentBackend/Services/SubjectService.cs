using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Services.Interfaces;

namespace TeamDevelopmentBackend.Services
{
    public class SubjectService : ISubjectService
    {
        private DefaultDBContext _dbContext;
        public SubjectService(DefaultDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public SubjectDbModel[] GetSubjects()
        {
            return _dbContext.Subjects.ToArray();
        }
        public async Task AddSubject(string name)
        {
            try
            {
                _dbContext.Subjects.Add(new SubjectDbModel { Id = new Guid(), Name = name });
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Subject with this name already exists!");
            }
        }

        public async Task DeleteSubject(Guid Id)
        {
            var subject=await _dbContext.Subjects.FindAsync(Id);
            Console.WriteLine(subject);
            if (subject != null) {
                var lessons = _dbContext.Lessons.Where(x => x.SubjectId == Id && x.StartDate >= DateOnly.FromDateTime(DateTime.Now)).ToList();
                var lessons2 = _dbContext.Lessons.Where(x => x.SubjectId == Id && x.StartDate < DateOnly.FromDateTime(DateTime.Now)).ToList();
                if (lessons2.Count != 0)
                {
                    throw new InvalidOperationException("There is lesson in the past with this subject!");
                }
                else
                {
                    lessons.ForEach(x => _dbContext.Remove(x));
                    _dbContext.Subjects.Remove(subject);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else {
                throw new ArgumentException("There is no subject with this ID!");
            }
            
        }
    }
    }

