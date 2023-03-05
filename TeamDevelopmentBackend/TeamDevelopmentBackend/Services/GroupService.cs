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
            _dbContext.Groups.Add(new GroupDbModel { Id=new Guid(), Name = groupName.Name });
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteGroup(Guid id)
        {
            var group = _dbContext.Teachers.FirstOrDefault(x => x.Id == id);
            try
            {
                _dbContext.Teachers.Remove(group);
                await _dbContext.SaveChangesAsync();
            }
            catch
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
