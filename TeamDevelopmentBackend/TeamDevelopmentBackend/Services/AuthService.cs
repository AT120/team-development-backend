using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO.Auth;
using TeamDevelopmentBackend.Services.Interfaces.Auth;

namespace TeamDevelopmentBackend.Services;

public class AuthService : IAuthService
{

    private readonly DefaultDBContext _dbcontext;
    private readonly ITokenIssuanceService _tokenService;

    private static byte[] Hash(string data) => SHA256.HashData(Encoding.Unicode.GetBytes(data));

    public AuthService(
        DefaultDBContext dbcontext,
        ITokenIssuanceService tokenservice) 
    {
        _dbcontext = dbcontext;
        _tokenService = tokenservice;
    }

    public async Task<TokenPairModel> Login(LoginCredsModel creds)
    {
        var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Login == creds.Email)
            ?? throw new BackendException("Неверный логин или пароль", 401);
        
        if (!user.PasswordHash.SequenceEqual(Hash(creds.Password)))
            throw new BackendException("Неверный логин или пароль", 401);

        return _tokenService.GetTokenPair(user);    
    }

    public void Logout(ulong refreshTokenId)
    {
        _tokenService.InvalidateRefreshToken(refreshTokenId);
    }

    public async Task<TokenPairModel> Register(RegisterModel creds)
    {
        var user = new UserDbModel{
            Id = new Guid(),
            Login = creds.Email,
            PasswordHash = Hash(creds.Password),
            Name = creds.Name,
            Role = Role.Student
        };

        await _dbcontext.Users.AddAsync(user);
        await _dbcontext.SaveChangesAsync();

        return _tokenService.GetTokenPair(user);
    }
}