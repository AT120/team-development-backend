using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TeamDevelopmentBackend.Filters;
using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Services;
using TeamDevelopmentBackend.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connection = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new NullReferenceException("Specify connetction string in appsetings file!");
builder.Services.AddDbContext<DefaultDBContext> (options => options.UseNpgsql(connection));

builder.Services.AddControllers();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddSingleton<IGlobalCounter<ulong>, GlobalCounterService>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option => {
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.OperationFilter<AuthRequiredFilter>();
});


TokenParameters.Issuer = builder.Configuration["JwtSettings:ValidIssuer"]
    ?? throw new NullReferenceException("Specify valid issuer in appsetings file!");

TokenParameters.Lifetime = Int32.Parse(
    builder.Configuration["JwtSettings:Lifetime"] 
        ?? throw new NullReferenceException("Specify lifetime of token in appsetings file!")
);

TokenParameters.Key = new SymmetricSecurityKey(
    Encoding.ASCII.GetBytes(
        builder.Configuration["JwtSettings:SecretKey"]
            ?? throw new NullReferenceException("Specify secret key in appsetings file!")
    )
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => 
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = TokenParameters.Issuer,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = TokenParameters.Key
        });




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
