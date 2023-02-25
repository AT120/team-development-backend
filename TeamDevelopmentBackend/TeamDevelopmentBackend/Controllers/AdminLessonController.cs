using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Model.DTO;
using TeamDevelopmentBackend.Services;

namespace TeamDevelopmentBackend.Controllers
{
    [Route("api/lesson")]
    [ApiController]
    public class AdminLessonController : ControllerBase
    {
        private ILessonService _lessonService;
        public AdminLessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LessonDTOModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _lessonService.AddLesson(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 409);
            }
        }
        [HttpPut("{lessonId}")]
        public async Task<IActionResult> Put(Guid lessonId, LessonEditDTOModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _lessonService.EditLesson(lessonId, model);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 409);
            }
        }

        [HttpDelete("{lessonId}")]
        public async Task<IActionResult> Delete(Guid lessonId)
        {
            try
            {
                await _lessonService.DeleteLesson(lessonId);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 404);
            }
        }
    }
}
