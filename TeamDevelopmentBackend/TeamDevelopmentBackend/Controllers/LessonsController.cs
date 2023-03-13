using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Model.DTO;
using TeamDevelopmentBackend.Model.DTO.Schedule;
using TeamDevelopmentBackend.Services.Interfaces;


namespace TeamDevelopmentBackend.Controllers
{
    [Route("api/lessons")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        private readonly IScheduleService _scheduleService;

        public LessonsController(ILessonService lessonService,
                                 IScheduleService scheduleService)
        {
            _lessonService = lessonService;
            _scheduleService = scheduleService;
        }

        [HttpGet("{date}")]
        public async Task<ActionResult<ScheduleModel>> GetSchedule(DateTime date,
                                                                   Guid? roomID,
                                                                   Guid? groupID,
                                                                   Guid? teacherID,
                                                                   Guid? subjectID)
            
        {
            try
            {
                ScheduleModel schedule = await _scheduleService.GetWeeklySchedule(
                        DateOnly.FromDateTime(date),
                        roomID,
                        groupID,
                        teacherID,
                        subjectID
                );
                return schedule;
            }
            catch
            {
                return StatusCode(500);
            }

        }


        [HttpPost]
        [Authorize]
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
            catch (InvalidOperationException ex)
            {
                return Problem(ex.Message, statusCode: 409);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message, statusCode: 400);
            }
        }

        [HttpPut("{lessonId}")]
        [Authorize]
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
            catch (RankException ex)
            {
                return Problem(ex.Message, statusCode: 404);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message, statusCode: 400);
            }
            catch (InvalidOperationException ex)
            {
                return Problem(ex.Message, statusCode: 409);
            }
        }

        [HttpDelete("{lessonId}")]
        [Authorize]
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
