using TeamDevelopmentBackend.Model;

namespace TeamDevelopmentBackend.Services
{
    public interface IGroupService
    {
        public Task AddGroup(string groupName);
        public Task DeleteGroup(Guid id);
        public GroupDbModel[] GetGroups();
    }
}
