using TeamDevelopmentBackend.Model.DTO.Schedule;

namespace TeamDevelopmentBackend.Services;

public interface IScheduleService
{
    public Task<ScheduleModel> GetWeeklySchedule(DateOnly date,
                                                 Guid? roomID,
                                                 Guid? groupID,
                                                 Guid? teacherID,
                                                 Guid? subjectID);
}