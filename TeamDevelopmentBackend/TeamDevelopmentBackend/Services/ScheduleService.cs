using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO.Schedule;
using TeamDevelopmentBackend.Services.Interfaces;

namespace TeamDevelopmentBackend.Services;

public class ScheduleService : IScheduleService
{
    private readonly DefaultDBContext _dbcontext;

    public ScheduleService(DefaultDBContext context)
    {
        _dbcontext = context;
    }

    private readonly ParameterExpression arg = Expression.Parameter(typeof(LessonDbModel));

    private Expression GetExpression(Guid? guid, string memberName)
    {
        
        if (guid is null)
            return Expression.Constant(true);

        
        MemberInfo member = typeof(LessonDbModel).GetProperty(memberName) 
            ?? throw new NullReferenceException();
            
        return Expression.Equal(
            Expression.Constant(guid),
            Expression.MakeMemberAccess(arg, member)
        );
    }
    

    private Expression<Func<LessonDbModel, bool>> getFilter(Guid? roomID, Guid? groupID, Guid? teacherID, Guid? subjectID)
    {
        Expression[] conditions = new Expression[4];
        conditions[0] = GetExpression(roomID, nameof(LessonDbModel.RoomId));
        conditions[1] = GetExpression(groupID, nameof(LessonDbModel.GroupId));
        conditions[2] = GetExpression(teacherID, nameof(LessonDbModel.TeacherId));
        conditions[3] = GetExpression(subjectID, nameof(LessonDbModel.SubjectId));

        Expression conjunction = conditions[0];
        for (int i = 1; i < conditions.Length; i++)
        {
            conjunction = Expression.AndAlso(conjunction, conditions[i]);
        }

        var filter =
            Expression.Lambda<Func<LessonDbModel, bool>>(
                conjunction,
                new List<ParameterExpression>() {arg}
            );

        return filter;
    }


    
    public async Task<ScheduleModel> GetWeeklySchedule(DateOnly date,
                                                       Guid? roomID,
                                                       Guid? groupID,
                                                       Guid? teacherID,
                                                       Guid? subjectID)
    {
        int currentDayOfWeek = (int)date.DayOfWeek;

        if (currentDayOfWeek == 0) //sunday
            currentDayOfWeek = 7;
        
        DateOnly monday = date.AddDays(1 - currentDayOfWeek);
        DateOnly sunday = date.AddDays(7 - currentDayOfWeek);
        var filter = getFilter(roomID, groupID, teacherID, subjectID);

        var days = await _dbcontext.Lessons
            .Include(l => l.Teacher)
            .Include(l => l.Group)
            .Include(l => l.Subject)
            .Include(l => l.Room)
            .ThenInclude(r => r.Building)
            .Where(l => sunday >= l.StartDate && monday < l.EndDate)
            .Where(filter)
            .GroupBy(l => l.WeekDay)
            .ToListAsync() ?? throw new NullReferenceException(); // TODO: better exception
    
        ScheduleModel schedule = new(days, monday);
        return schedule;
    }

}