using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO.Auth;
using TeamDevelopmentBackend.Services.Interfaces.Auth;

namespace TeamDevelopmentBackend.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IAuthService _authService;
    private readonly ITokenIssuanceService _tokenService;
    public AuthController(
        IAuthService authService,
        ITokenIssuanceService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenPairModel>> Login(LoginCredsModel creds)
    {

        try
        {
            return await _authService.Login(creds);
        }
        catch(BackendException be)
        {
            return Problem(be.Message, statusCode: be.HttpCode);
        }
        catch
        {
            return Problem("Unexpected behaivor", statusCode: 500); 
        }
    }

    [HttpPost("logout")]
    [Authorize]
    public ActionResult Logout()
    {
        try
        {
            _authService.Logout(
                ulong.Parse(User.FindFirst(ClaimsDefault.IssuedByTokenId).Value)
            );
        }
        catch
        {
            return Problem("Unexpected behaivor", statusCode: 500);
        }

        return Ok();
    }

    [HttpPost("register")]
    public async Task<ActionResult<TokenPairModel>> Register(RegisterModel creds)
    {
        try
        {
            return await _authService.Register(creds);
        }
        catch(DbUpdateException)
        {   
            return Problem($"Email {creds.Email} has been already used!", statusCode: 409);
        }
        catch
        {
            return Problem("Unexpected behaivor", statusCode: 500);
        }
    }

    [HttpGet("refresh")]
    [Authorize(Policies.RefreshOnly)]
    public ActionResult<TokenPairModel> Refresh()
    {
        // try
        // {
        //     TokenType type = User.FindFirst(ClaimsDefault.TokenType).Value;
        //     ulong ParentTokenId = User.FindFirst(ClaimsDefault.TokenId)
            // _tokenService.InvalidateRefreshToken
        //     _tokenService.GetTokenPair()
        // }
        return Problem("This method has not been yet implemented", statusCode: 501); 
    }
    
} 