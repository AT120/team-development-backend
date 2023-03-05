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
        public async Task GiveUserARole(LoginDTOModel userLogin, Role role, Guid? teacherId=null)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Login == userLogin.Login);

            if (user == null)
            {
                throw new ArgumentException("There is no user with this login!");
            }
            else
            {
                if(user.Role == Role.Teacher && role!=Role.Teacher) 
                {
                    user.Id = new Guid();
                }
               else if (role == Role.Teacher)
                {
                    var teacher = _dbContext.Users.FirstOrDefault(x => x.Id == teacherId);
                    if (teacher == null)
                    {
                        throw new Exception("There is no teacher with this Id!");
                    }
                    user.Id = teacher.Id;
                }

                user.Role = role;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
