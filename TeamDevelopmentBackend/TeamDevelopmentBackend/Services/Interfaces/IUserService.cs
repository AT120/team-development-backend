using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO.User;

namespace TeamDevelopmentBackend.Services.Interfaces
{
    public interface IUserService
    {
        public Task GiveUserARole(LoginDTOModel userLogin, Role role);
    }
}
