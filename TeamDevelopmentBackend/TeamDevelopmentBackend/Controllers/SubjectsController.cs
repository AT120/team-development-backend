using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO;
using TeamDevelopmentBackend.Services;

namespace TeamDevelopmentBackend.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }


        [HttpGet]
        public ActionResult<ICollection<SubjectDbModel>> GetSubjectsList()
        {
            return _subjectService.GetSubjects();
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(NameModel subject)
        {
            try
            {
                await _subjectService.AddSubject(subject.Name);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 409);
            }
        }


        [HttpDelete("{subjectId}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid subjectId)
        {
            try
            {
                await _subjectService.DeleteSubject(subjectId);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 404);
            }
        }


    }
}
