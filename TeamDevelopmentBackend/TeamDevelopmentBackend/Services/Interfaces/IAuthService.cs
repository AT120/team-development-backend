using TeamDevelopmentBackend.Model.DTO.Auth;

namespace TeamDevelopmentBackend.Services.Interfaces;

public interface IAuthService
{
    Task<TokenPairModel> Login(LoginCredsModel creds);
    Task<TokenPairModel> Register(RegisterModel creds);
    void Logout(ulong refreshTokenId);

}