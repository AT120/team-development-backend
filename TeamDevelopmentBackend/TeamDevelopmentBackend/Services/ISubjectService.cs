using TeamDevelopmentBackend.Model;

namespace TeamDevelopmentBackend.Services
{
    public interface ISubjectService
    {
        public Task AddSubject(string name);
        public Task DeleteSubject(Guid Id);
    }
}
