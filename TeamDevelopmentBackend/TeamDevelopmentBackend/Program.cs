using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TeamDevelopmentBackend.Filters;
using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Services;
using TeamDevelopmentBackend.Services.Interfaces;
using TeamDevelopmentBackend.Services.Interfaces.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connection = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new NullReferenceException("Specify connetction string in appsetings file!");
builder.Services.AddDbContext<DefaultDBContext>(options => options.UseNpgsql(connection));

builder.Services.AddControllers();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IGlobalCounter<ulong>, GlobalCounterService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenIssuanceService, TokenIssuanceService>();
builder.Services.AddScoped<IGlobalCounter<ulong>, GlobalCounterService>();
builder.Services.AddEndpointsApiExplorer();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader();
                      });
});
builder.Services.AddSwaggerGen(option =>
{
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

TokenParameters.AccessLifetime = Int32.Parse(
    builder.Configuration["JwtSettings:AccessLifetime"]
        ?? throw new NullReferenceException("Specify lifetime of token in appsetings file!")
);

TokenParameters.RefreshLifetime = Int32.Parse(
    builder.Configuration["JwtSettings:RefreshLifetime"]
        ?? throw new NullReferenceException("Specify lifetime of token in appsetings file!")
);

TokenParameters.Key = new SymmetricSecurityKey(
    Encoding.ASCII.GetBytes(
        builder.Configuration["JwtSettings:SecretKey"]
            ?? throw new NullReferenceException("Specify secret key in appsetings file!")
    )
);

if (TokenParameters.Key.KeySize < 128)
    throw new ArgumentException("Secret key have to be at least 8 symbols!");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = ValidatorsPile.ValidateTokenParent
        };
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = TokenParameters.Issuer,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = TokenParameters.Key
        };
    });



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.RefreshOnly, policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim(ClaimType.TokenType, TokenType.Refresh.ToString());
    });

    options.AddPolicy(Policies.Admin, policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireAssertion(ValidatorsPile.ValidateAdminRole);
        policy.RequireClaim(ClaimType.TokenType, TokenType.Access.ToString());
    });

    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .RequireClaim(ClaimType.TokenType, TokenType.Access.ToString())
        .Build();
});



var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
