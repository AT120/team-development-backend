using System.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Model;
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
        public ActionResult<ICollection<GroupDbModel>> Get()
        {
            return _groupService.GetGroups(); 
        }

        [HttpPost]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Delete(Guid groupId)
        {
            try
            {
                await _groupService.DeleteGroup(groupId);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 404);
            }
        }
    }
}
