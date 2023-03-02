using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO.User;

namespace TeamDevelopmentBackend.Services
{
    public class UserService : IUserService
    {
        private DefaultDBContext _dbContext;
        public UserService(DefaultDBContext dBContext) 
        {
        _dbContext= dBContext;
        }
        public async Task GiveUserARole(LoginDTOModel userLogin, Role role)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Login == userLogin.Login);
            if(user == null)
            {
                throw new ArgumentException("There is no user with this login!");
            }
            else
            {
                user.Role = role;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
