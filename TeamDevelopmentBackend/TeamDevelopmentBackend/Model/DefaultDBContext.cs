using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using TeamDevelopmentBackend.Models;

public class DefaultDBContext: DbContext
{
    public DbSet<SubjectModel> Subjects { get; set; }

    public DefaultDBContext(DbContextOptions<DefaultDBContext> options): base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

}
