using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO.User;
using TeamDevelopmentBackend.Services.Interfaces;

namespace TeamDevelopmentBackend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("info")]
        [Authorize]
        public ActionResult<UserInfoModel> GetUserInfo()
        {
            return Problem("This method has not been yet implemented", statusCode: 501);
        }

        [HttpPut("group")]
        [Authorize]
        public ActionResult EditGroup(GroupEditModel group)
        {
            return Problem("This method has not been yet implemented", statusCode: 501);
        }

        [HttpPost("{role}")]
        [Authorize]
        public async Task<IActionResult> Put(Role role, LoginDTOModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await _userService.GiveUserARole(model, role);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message, statusCode: 404);
            }
        }
    }
}
