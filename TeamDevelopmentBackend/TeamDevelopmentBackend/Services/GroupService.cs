using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO;
using TeamDevelopmentBackend.Services.Interfaces;

namespace TeamDevelopmentBackend.Services
{
    public class GroupService : IGroupService
    {
        private DefaultDBContext _dbContext;
        public  GroupService(DefaultDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddGroup(NameModel groupName)
        {
            try
            {
               await _dbContext.Groups.AddAsync(new GroupDbModel { Id = new Guid(), Name = groupName.Name });
               await _dbContext.SaveChangesAsync();
            }
            catch 
            {
                throw new Exception("There is already group with this name!");
            }
        }

        public async Task DeleteGroup(Guid id)
        {
            var group = _dbContext.Groups.FirstOrDefault(x => x.Id == id);
            if (group != null) { 
                var lessons = _dbContext.Lessons.Where(x => x.GroupId == id && x.StartDate >= DateOnly.FromDateTime(DateTime.Now)).ToList();
                lessons.ForEach(x => _dbContext.Remove(x));
                var users = _dbContext.Users.Where(x => x.DefaultFilterId==id).ToList();
                users.ForEach(x => _dbContext.Remove(x));
                _dbContext.Groups.Remove(group);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("There is no group with this ID!");
            }
        }

        public GroupDbModel[] GetGroups()
        {
            return _dbContext.Groups.ToArray();
        }
    }
}
