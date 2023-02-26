using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO;
using TeamDevelopmentBackend.Services;

namespace TeamDevelopmentBackend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private IUserService _userService;
        public AdminUserController(IUserService userService) 
        {
            _userService = userService;
        }
        [HttpPost("{role}")]
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
                return Problem(ex.Message,statusCode:404);
            }
        } 
    }
}
