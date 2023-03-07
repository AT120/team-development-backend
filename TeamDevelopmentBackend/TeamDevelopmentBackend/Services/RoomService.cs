using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO;

namespace TeamDevelopmentBackend.Services
{
    public class RoomService : IRoomService
    {
        private DefaultDBContext _dbContext;
        public RoomService(DefaultDBContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task AddRoom(NameModel name, Guid BuildingId)
        {
           var building = _dbContext.Buildings.FirstOrDefault(x=>x.Id == BuildingId);
            if (building == null)
            {
                throw new InvalidOperationException("There is no building with this Id");
            }
            else
            {
                var room = new RoomDbModel { Id = new Guid(), Building = building, Name=name.Name }; 
                await _dbContext.Rooms.AddAsync(room);
                building.Rooms.Add(room);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveRoom(Guid RoomId)
        {
           var room = _dbContext.Rooms.FirstOrDefault(x=>x.Id == RoomId);
            if (room == null)
            {
                throw new InvalidOperationException("There is no room with this Id!");
            }
            else
            {
                _dbContext.Rooms.Remove(room);
                await _dbContext.SaveChangesAsync();
            }       

        }

        public RoomDTOModel[] GetRooms(Guid buildingId)
        {
            var building = _dbContext.Buildings.FirstOrDefault(x => x.Id == buildingId);
            if (building == null)
            {
                throw new InvalidOperationException("There is no building with this Id!");
            }
            else
            {
                var rooms = _dbContext.Rooms.Where(x => x.Building == building).ToArray();
                RoomDTOModel[] answer = new RoomDTOModel[rooms.Length];
                for (int i = 0; i < rooms.Length; i++)
                {
                    answer[i] = new RoomDTOModel(rooms[i]);
                }
                return answer;
            }
        }
    }
}
