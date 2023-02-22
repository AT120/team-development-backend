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
    public DefaultDBContext(DbContextOptions<DefaultDBContext> options): base(options)
    {
      //  Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LessonDbModel>().HasIndex(x => new { x.TimeSlot, x.Date, x.RoomId }).IsUnique();
        modelBuilder.Entity<LessonDbModel>().HasIndex(x => new { x.TimeSlot, x.Date, x.TeacherId }).IsUnique();
        modelBuilder.Entity<LessonDbModel>().HasIndex(x => new { x.TimeSlot, x.Date, x.GroupId }).IsUnique();
    }

}
