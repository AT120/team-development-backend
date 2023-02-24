using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Model.DTO.Schedule;
using TeamDevelopmentBackend.Services;

namespace TeamDevelopmentBackend.Controllers;

[ApiController]
[Route("api/schedule")]
public class ScheduleController : ControllerBase
{ 
     private readonly IScheduleService _scheduleService;

     public ScheduleController(IScheduleService scheduleService)
     {
          _scheduleService = scheduleService;
     } 

     [HttpGet("{date}")]
     public async Task<IActionResult> GetSchedule(DateTime date,
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
               return new JsonResult(schedule);
          }
          catch
          {
               return StatusCode(500);
          }

     }


    
}