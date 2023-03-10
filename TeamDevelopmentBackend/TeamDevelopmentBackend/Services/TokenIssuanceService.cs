using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TeamDevelopmentBackend.Model;
using TeamDevelopmentBackend.Model.DTO.Auth;
using TeamDevelopmentBackend.Services.Interfaces;
using TeamDevelopmentBackend.Services.Interfaces.Auth;

namespace TeamDevelopmentBackend.Services;

public class TokenIssuanceService : ITokenIssuanceService
{

    private readonly DefaultDBContext _dbcontext;
    private readonly IGlobalCounter<ulong> _globalCounter;

    public TokenIssuanceService(DefaultDBContext dbcontext, IGlobalCounter<ulong> counter)
    {
        _dbcontext = dbcontext;
        _globalCounter = counter;
    }


    private Claim[] GetClaims(
        UserDbModel user,
        TokenType type,
        ulong IssuedByTokenId)
    {
        ulong tokenId = (type == TokenType.Refresh) ? _globalCounter.Next() : 0;

        Claim[] claims = {
            new Claim(ClaimType.UserId, user.Id.ToString()),
            new Claim(ClaimType.TokenId, tokenId.ToString()),
            new Claim(ClaimType.IssuedByTokenId, IssuedByTokenId.ToString()),
            new Claim(ClaimType.TokenType, type.ToString())
        };

        return claims;

    }


    private string GenerateToken(UserDbModel user, TokenType type, ulong refreshTokenId)
    {
        Claim[] claims = GetClaims(user, type, refreshTokenId);
        int lifetime = (type == TokenType.Refresh) ? TokenParameters.RefreshLifetime : TokenParameters.AccessLifetime;
        var now = DateTime.UtcNow;
        var token = new JwtSecurityToken(
            issuer: TokenParameters.Issuer,
            notBefore: now,
            expires: now.AddMinutes(lifetime),
            claims: claims,
            signingCredentials: new SigningCredentials(
                TokenParameters.Key,
                SecurityAlgorithms.HmacSha256
            )
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    private TokenPairModel GenerateTokenPair(UserDbModel user, ulong parentRefreshTokenId)
    {
        var tokens = new TokenPairModel
        {
            RefreshToken = GenerateToken(user, TokenType.Refresh, parentRefreshTokenId),
            AccessToken = GenerateToken(user, TokenType.Access, parentRefreshTokenId)
        };

        _dbcontext.IssuedTokens.Add(
            new IssuedTokenDbModel { RefreshTokenId = parentRefreshTokenId }
        );
        _dbcontext.SaveChanges();

        return tokens;
    }


    public TokenPairModel GetTokenPair(Guid userId, ulong parentRefreshTokenId)
    {
        var user = _dbcontext.Users.Find(userId)
            ?? throw new BackendException("Пользователь был удален", 410);

        return GenerateTokenPair(user, parentRefreshTokenId);
    }


    public TokenPairModel GetTokenPair(UserDbModel user)
    {
        return GenerateTokenPair(user, _globalCounter.Next());
    }


    public void InvalidateRefreshToken(ulong parentRefreshTokenId)
    {
        _dbcontext.IssuedTokens.Remove(
            new IssuedTokenDbModel { RefreshTokenId = parentRefreshTokenId }
        );

        _dbcontext.SaveChanges();
    }
}