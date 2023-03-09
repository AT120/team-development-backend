using TeamDevelopmentBackend.Model;

namespace TeamDevelopmentBackend.Services.Interfaces
{
    public interface ISubjectService
    {
        public Task AddSubject(string name);
        public Task DeleteSubject(Guid Id);
        public SubjectDbModel[] GetSubjects();
    }
}
