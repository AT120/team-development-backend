using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Services;

namespace TeamDevelopmentBackend.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //TODO:
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(string name) //TODO: wrap name in model
        {
            try
            {
                await _groupService.AddGroup(name);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem("", statusCode: 502);
            }
        }

        [HttpDelete("{groupId}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _groupService.DeleteGroup(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 404);
            }
        }
    }
}
