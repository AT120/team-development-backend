using TeamDevelopmentBackend.Model.DTO;

namespace TeamDevelopmentBackend.Services
{
    public interface IBuildingService
    {
        public Task AddBuilding(NameModel name);
        public Task RemoveBuilding(Guid Id);
        public BuildingDTOModel[] GetBuildings();
    }
}
