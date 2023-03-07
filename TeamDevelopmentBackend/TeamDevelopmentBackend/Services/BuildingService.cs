using System.Xml.Linq;
using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO;

namespace TeamDevelopmentBackend.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly DefaultDBContext _dbContext;
        public BuildingService(DefaultDBContext dBContext) 
        {
        _dbContext= dBContext;
        }
        public async Task AddBuilding(NameModel name)
        {
           await _dbContext.Buildings.AddAsync(new BuildingDbModel { Id=new Guid(),Name=name.Name});
           await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveBuilding(Guid Id)
        {
            var building = _dbContext.Buildings.FirstOrDefault(x => x.Id == Id);
            if (building != null)
            {
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
