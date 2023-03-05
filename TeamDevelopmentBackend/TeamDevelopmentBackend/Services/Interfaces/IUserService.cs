using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO.User;

namespace TeamDevelopmentBackend.Services.Interfaces
{
    public interface IUserService
    {
        public Task GiveUserARole(string userLogin, Role role, Guid? teacherId=null);
    }
}
