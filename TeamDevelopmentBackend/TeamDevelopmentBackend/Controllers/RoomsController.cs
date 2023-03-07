using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Model.DTO;
using TeamDevelopmentBackend.Services;

namespace TeamDevelopmentBackend.Controllers;


[ApiController]
[Route("api/buildings/{buildingId}/rooms")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;
    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }
    [HttpGet]
    public ActionResult<ICollection<RoomDTOModel>> GetRooms(Guid buildingId)
    {
        try
        {
            return _roomService.GetRooms(buildingId);
        }
        catch(Exception ex)
        {
            return Problem(ex.Message, statusCode: 404);
        }
    }

    [HttpPost]
    //[Authorize]
    public async Task<IActionResult> CreateRoom(Guid buildingId,NameModel room)
    {
        try
        {
            await _roomService.AddRoom(room,buildingId);
            return Ok();
        }
        catch(Exception ex)
        {
            return Problem(ex.Message, statusCode: 404);
        }
    }

    [HttpDelete("/api/buildings/rooms/{roomId}")]
    //[Authorize]
    public async Task<IActionResult> DeleteRoom(Guid roomId)
    {
        try
        {
            await _roomService.RemoveRoom(roomId);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, statusCode: 404);
        }
    }
}