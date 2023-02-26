using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Services;

namespace TeamDevelopmentBackend.Controllers
{
    [Route("api/subject")]
    [ApiController]
    public class AdminSubjectController : ControllerBase
    {
        private ISubjectService _subjectService;
        public AdminSubjectController(ISubjectService subjectService) 
        {
            _subjectService= subjectService;
        }
        [HttpPost("{name}")]
        public async Task<IActionResult> Post(string name)
        {

            try
            {
                await _subjectService.AddSubject(name);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 409);
            }
        }
        [HttpDelete("{subjectId}")]
        public async Task<IActionResult> Delete(Guid subjectId)
        {
            try
            {
                await _subjectService.DeleteSubject(subjectId);
                return Ok();   
            }
            catch(Exception ex) 
            {
                return Problem(ex.Message, statusCode: 404);
            }
        }


    }
}
