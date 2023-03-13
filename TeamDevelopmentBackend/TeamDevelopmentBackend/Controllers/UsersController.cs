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
            try
            {
                return _userService.GetUserInfo(ClaimsExtractor.GetUserId(User));
            }
            catch(BackendException be)
            {
                return Problem(be.Message, statusCode: be.HttpCode);
            }
            catch
            {
                return Problem("Unexpected behaivor", statusCode: 500); 
            }
        }

        [HttpPut("group")]
        [Authorize]
        public ActionResult EditGroup(GroupEditModel group)
        {
            try
            {
                _userService.EditGroup(group.GroupId, ClaimsExtractor.GetUserId(User));
                return Ok();
            }
            catch(BackendException be)
            {
                return Problem(be.Message, statusCode: be.HttpCode);
            }
            catch
            {
                return Problem("Unexpected behaivor", statusCode: 500); 
            }
        }

        [HttpPut("{login}/role")]
        [Authorize(Policies.Admin)]
        public async Task<IActionResult> Put(string login, RoleEditModel role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await _userService.GiveUserARole(login, role.Role, role.TeacherId);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 404);
            } 
        }
    }
}
