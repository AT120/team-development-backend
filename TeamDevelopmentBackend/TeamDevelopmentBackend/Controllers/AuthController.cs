using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamDevelopmentBackend.Model.DTO.Auth;

namespace TeamDevelopmentBackend.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{

    [HttpPost("login")]
    public ActionResult<TokenPairModel> Login(LoginCredsModel creds)
    {
        return Problem("This method has not been yet implemented", statusCode: 501); 
    }

    [HttpPost("logout")]
    [Authorize]
    public ActionResult Logout()
    {
        return Problem("This method has not been yet implemented", statusCode: 501); 
    }

    [HttpPost("register")]
    public ActionResult<TokenPairModel> Register(RegisterModel creds)
    {
        return Problem("This method has not been yet implemented", statusCode: 501); 
    }

    [HttpGet("refresh")] //TODO: authorize?
    public ActionResult<TokenPairModel> Refresh(RefreshTokenModel token)
    {
        return Problem("This method has not been yet implemented", statusCode: 501); 
    }

    
} 