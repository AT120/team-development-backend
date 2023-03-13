using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO.User;
using TeamDevelopmentBackend.Services.Interfaces;

namespace TeamDevelopmentBackend.Services
{
    public class UserService : IUserService
    {
        private DefaultDBContext _dbContext;
        public UserService(DefaultDBContext dBContext) 
        {
        _dbContext= dBContext;
        }

        public void EditGroup(Guid groupId, Guid userId)
        {
            var user = _dbContext.Users.Find(userId) 
                ?? throw new BackendException("User does not exist", 410);
            
            if (user.Role != Role.Student)
                throw new BackendException("Only students can change their group", 403);

            if (_dbContext.Groups.Find(groupId) is null)
                throw new BackendException($"Group with id {groupId} does not exist", 404);

            user.DefaultFilterId = groupId;
            _dbContext.SaveChanges();
        }

        public UserInfoModel GetUserInfo(Guid userId)
        {
            var user = _dbContext.Users.Find(userId) 
                ?? throw new BackendException("User does not exist", 410);
            
            return new UserInfoModel 
            {
                DefaultId = user.DefaultFilterId,
                Login = user.Login,
                Role = user.Role,
                Name = user.Name
            };

        }

        public async Task GiveUserARole(string userLogin, Role role, Guid? teacherId=null)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Login == userLogin);

            if (user == null)
            {
                throw new ArgumentException("There is no user with this login!");
            }
            else
            {
                if (role == Role.Teacher)
                {
                    var teacher = _dbContext.Teachers.FirstOrDefault(x => x.Id == teacherId); 
                    if (teacher == null)
                    {
                        throw new Exception("There is no teacher with this Id!");
                    }
                    user.DefaultFilterId = teacher.Id;
                }
               else if(user.Role == Role.Teacher) 
                {
                    user.DefaultFilterId = null;
                }
                user.Role = role;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
