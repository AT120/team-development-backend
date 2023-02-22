using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO.Schedule;

namespace TeamDevelopmentBackend.Services;

public class ScheduleService : IScheduleService
{
    private readonly DefaultDBContext _dbcontext;

    public ScheduleService(DefaultDBContext context)
    {
        _dbcontext = context;
    }
    
    public async Task<ScheduleModel> GetWeeklySchedule(DateOnly date)
    {
        int currentDayOfWeek = (int)date.DayOfWeek;

        if (currentDayOfWeek == 0) //sunday
            currentDayOfWeek = 7;
        
        DateOnly monday = date.AddDays(1 - currentDayOfWeek);
        DateOnly sunday = date.AddDays(7 - currentDayOfWeek);
        

        var days = await _dbcontext.Lessons
            .Include(l => l.Teacher)
            .Include(l => l.Group)
            .Include(l => l.Subject)
            .Include(l => l.Room)
            .ThenInclude(r => r.Building)
            .Where(l => l.Date >= monday && l.Date <= sunday)
            .GroupBy(l => l.Date)
            .ToListAsync() ?? throw new NullReferenceException(); // TODO: better exception
    
        ScheduleModel schedule = new(days);
        return schedule;
    }

}