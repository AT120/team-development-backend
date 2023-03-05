using System.ComponentModel;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using TeamDevelopmentBackend.Model;

public class DefaultDBContext: DbContext
{
    public DbSet<SubjectDbModel> Subjects { get; set; }
    public DbSet<BuildingDbModel> Buildings { get; set; }
    public DbSet<GroupDbModel> Groups { get; set; }
    public DbSet<LessonDbModel> Lessons { get; set; }
    public DbSet<RoomDbModel> Rooms { get; set; }
    public DbSet<TeacherDbModel> Teachers { get; set; }
    public DbSet<UserDbModel> Users { get; set; }
    public DbSet<IssuedTokenDbModel> IssuedTokens { get; set; }
    public DbSet<CounterStorageDbModel> Counter { get; set; }

    public DefaultDBContext(DbContextOptions<DefaultDBContext> options): base(options)
    {
      //  Database.EnsureCreated();
    }
    
    public async Task<bool> CheckIfCanBeAddedInDatabase(LessonDbModel model)
    {
        
        var checker = await this.Lessons.Where(x => 
            x.TimeSlot == model.TimeSlot && 
            x.WeekDay == model.WeekDay && 
            model.StartDate < x.EndDate && 
            model.StartDate >= x.StartDate &&
            (x.TeacherId == model.TeacherId || x.GroupId == model.GroupId || x.RoomId == model.RoomId)
        ).FirstOrDefaultAsync();
        
        Console.WriteLine(checker == null);
        return checker == null;
    } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LessonDbModel>().Property(l => l.EndDate).HasDefaultValue(endDateDefault);
        modelBuilder.Entity<UserDbModel>().HasIndex(u => u.Login).IsUnique();
    }

    private readonly DateOnly endDateDefault = new(9999, 12, 31);

}
