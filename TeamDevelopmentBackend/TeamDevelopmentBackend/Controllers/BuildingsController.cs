using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Model.DTO;
using TeamDevelopmentBackend.Services;

namespace TeamDevelopmentBackend.Controllers;


[ApiController]
[Route("api/buildings")]
public class BuildingsController : ControllerBase
{
    private IBuildingService _buildingsService;
    public BuildingsController(IBuildingService buildingsService)
    {
        _buildingsService = buildingsService;
    }

    [HttpGet]
    public ActionResult<ICollection<BuildingDTOModel>> GetBuildings()
    {
        return Problem("This method has not been yet implemented", statusCode: 501); 
    }

    [HttpPost]
   // [Authorize]
    public async Task<IActionResult> CreateBuilding(NameModel building)
    {

        try
        {
            await _buildingsService.AddBuilding(building);
            return Ok();
        }
        catch
        {
            return Problem("This method has not been yet implemented", statusCode: 501);
        }
    }

    [HttpDelete("{buildingId}")]
    //[Authorize]
    public async Task<IActionResult> DeleteBuilding(Guid buildingId)
    {
        try
        {
            await _buildingsService.RemoveBuilding(buildingId);
            return Ok();
        }
        catch(Exception ex)
        {
            return Problem(ex.Message,statusCode: 404);
        }
    }
}