using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Model;
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
        return _buildingsService.GetBuildings();
    }

    [HttpPost]
    [Authorize(Policies.Admin)]
    public async Task<IActionResult> CreateBuilding(NameModel building)
    {

        try
        {
            await _buildingsService.AddBuilding(building);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, statusCode: 409);
        }
    }

    [HttpDelete("{buildingId}")]
    [Authorize(Policies.Admin)]
    public async Task<IActionResult> DeleteBuilding(Guid buildingId)
    {
        try
        {
            await _buildingsService.RemoveBuilding(buildingId);
            return Ok();
        }
        catch(ArgumentException ex)
        {
            return Problem(ex.Message,statusCode: 404);
        }
        catch(InvalidOperationException ex)
        {
            return Problem(ex.Message, statusCode: 400);
        }
    }
}