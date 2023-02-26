using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO;

namespace TeamDevelopmentBackend.Services
{
    public interface IUserService
    {
        public Task GiveUserARole(LoginDTOModel userLogin, Role role);
    }
}
