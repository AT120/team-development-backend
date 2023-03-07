using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO;

namespace TeamDevelopmentBackend.Services
{
    public interface IGroupService
    {
        public Task AddGroup(NameModel groupName);
        public Task DeleteGroup(Guid id);
        public GroupDbModel[] GetGroups();
    }
}
