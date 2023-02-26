using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Services;

namespace TeamDevelopmentBackend.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class AdminGroupController : ControllerBase
    {
        private IGroupService _groupService;
        public AdminGroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpPost("{name}")]
        public async Task<IActionResult> Post(string name)
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
