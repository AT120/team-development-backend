using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Model.DTO;

namespace TeamDevelopmentBackend.Controllers;


[ApiController]
[Route("api/buildings")]
public class BuildingsController : ControllerBase
{
    [HttpGet]
    public ActionResult<ICollection<BuildingDTOModel>> GetBuildings()
    {
        return Problem("This method has not been yet implemented", statusCode: 501); 
    }

    [HttpPost]
    [Authorize]
    public ActionResult CreateBuilding(NameModel building)
    {
        return Problem("This method has not been yet implemented", statusCode: 501); 
    }

    [HttpDelete("{buildingId}")]
    public ActionResult DeleteBuilding(Guid buildingId)
    {
        return Problem("This method has not been yet implemented", statusCode: 501); 
    }
}