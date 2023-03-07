using TeamDevelopmentBackend.Model;

namespace TeamDevelopmentBackend.Services
{
    public interface ITeacherService
    {
        public Task AddTeacher(string name);
        public Task DeleteTeacher(Guid ID);
        public TeacherDbModel[] GetTeachers();
    }
}
