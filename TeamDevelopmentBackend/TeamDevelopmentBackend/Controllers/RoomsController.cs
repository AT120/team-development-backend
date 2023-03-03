using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Model.DTO;

namespace TeamDevelopmentBackend.Controllers;


[ApiController]
[Route("api/buildings/{buildingId}/rooms")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public ActionResult<ICollection<BuildingDTOModel>> GetRooms(Guid buildingId)
    {
        return Problem("This method has not been yet implemented", statusCode: 501); 
    }

    [HttpPost]
    [Authorize]
    public ActionResult CreateRoom(NameModel room)
    {
        return Problem("This method has not been yet implemented", statusCode: 501); 
    }

    [HttpDelete("/api/buildings/rooms/{roomId}")]
    public ActionResult DeleteRoom(Guid roomId)
    {
        return Problem("This method has not been yet implemented", statusCode: 501); 
    }
}