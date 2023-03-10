using TeamDevelopmentBackend.Model.DTO;

namespace TeamDevelopmentBackend.Services
{
    public interface IRoomService
    {
        public Task AddRoom(NameModel name, Guid BuildingId);
        public Task RemoveRoom(Guid RoomId);
        public RoomDTOModel[] GetRooms(Guid buildingId);
    }
}
