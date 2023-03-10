using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO;

namespace TeamDevelopmentBackend.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly DefaultDBContext _dbContext;
        private IRoomService _roomService;
        public BuildingService(DefaultDBContext dBContext, IRoomService roomService) 
        {
        _dbContext= dBContext;
        _roomService= roomService;
        }
        public async Task AddBuilding(NameModel name)
        {
           await _dbContext.Buildings.AddAsync(new BuildingDbModel { Id=new Guid(),Name=name.Name});
           await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveBuilding(Guid Id)
        {
            var building = _dbContext.Buildings.Include(u=>u.Rooms).FirstOrDefault(x => x.Id == Id);
            if (building != null)
            {
                foreach (var room in building.Rooms)
                {
                    await _roomService.RemoveRoom(room.Id);
                }
                _dbContext.Buildings.Remove(building);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("There is no building with this Id!"); 
            }
        }

        public BuildingDTOModel[] GetBuildings()
        {
            return _dbContext.Buildings.Select(x => new BuildingDTOModel(x)).ToArray();
        }
    }
}
