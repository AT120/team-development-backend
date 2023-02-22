﻿using TeamDevelopmentBackend.Model;

namespace TeamDevelopmentBackend.Services
{
    public class SubjectService : ISubjectService
    {
        private DefaultDBContext _dbContext;
        public SubjectService(DefaultDBContext dbContext)
        {
            _dbContext = dbContext;
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
            var subject= _dbContext.Subjects.FirstOrDefault(x => x.Id == Id);
            try
            {
                _dbContext.Subjects.Remove(subject);
                await _dbContext.SaveChangesAsync();
            }
            catch {
                throw new Exception("There is no subject with this ID!");
            }
            
        }
    }
    }
