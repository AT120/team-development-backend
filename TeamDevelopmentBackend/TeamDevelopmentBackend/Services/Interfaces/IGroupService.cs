namespace TeamDevelopmentBackend.Services.Interfaces
{
    public interface IGroupService
    {
        public Task AddGroup(string groupName);
        public Task DeleteGroup(Guid id);
    }
}
