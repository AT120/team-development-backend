using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO.Auth;

namespace TeamDevelopmentBackend.Services.Interfaces;

public interface ITokenIssuanceService 
{
    TokenPairModel GetTokenPair(Guid userId, ulong parentRefreshTokenId);
    TokenPairModel GetTokenPair(UserDbModel user);
    void InvalidateRefreshToken(ulong parentRefreshTokenId);
}