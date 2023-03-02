using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using TeamDevelopmentBackend.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DefaultDBContext> (options => options.UseNpgsql(connection));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
