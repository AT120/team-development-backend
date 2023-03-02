using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Services;
using TeamDevelopmentBackend.Model.DTO;
using TeamDevelopmentBackend.Model;

namespace TeamDevelopmentBackend.Controllers
{
    [Route("api/teachers")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public ActionResult<ICollection<TeacherDbModel>> GetTeachersList()
        {
            return Problem("This method has not been yet implemented", statusCode: 501); 
        }

        [HttpPost]
        public async Task<IActionResult> Post(NameModel teacher)
        {
            try
            {
                await _teacherService.AddTeacher(teacher.Name);
                return Ok();
            }
            catch
            {
                return Problem("", statusCode: 502);
            }
        }

        [HttpDelete("{subjectId}")]
        public async Task<IActionResult> Delete(Guid subjectId)
        {
            try
            {
                await _teacherService.DeleteTeacher(subjectId);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 404);
            }
        }
    }
}
