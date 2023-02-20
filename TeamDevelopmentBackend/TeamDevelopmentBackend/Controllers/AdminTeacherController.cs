using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Services;

namespace TeamDevelopmentBackend.Controllers
{
    [Route("api/teacher")]
    [ApiController]
    public class AdminTeacherController : ControllerBase
    {
        private ITeacherService _teacherService;
        public AdminTeacherController(ITeacherService teacherService) {
            _teacherService = teacherService;
        }
        [HttpPost("{name}")]
        public async Task<IActionResult> Post(string name)
        {

            try
            {
                await _teacherService.AddTeacher(name);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem("", statusCode: 502);
            }
        }
        [HttpDelete("subjectId")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _teacherService.DeleteTeacher(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 404);
            }
        }
    }
}
