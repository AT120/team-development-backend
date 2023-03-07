using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace TeamDevelopmentBackend.Model;

class TokenParameters
{
    private static string _issuer = "";
    public static string Issuer
    {
        get { return _issuer; }
        set
        {
            if (_issuer == "")
                _issuer = value;
        }
    }

    private static int _accessLifetime = -1;
    public static int AccessLifetime 
    {
        get { return _accessLifetime; }
        set
        {
            if (_accessLifetime == -1)
                _accessLifetime = value;
        }
    }


    private static int _refreshLifetime = -1;
    public static int RefreshLifetime 
    {
        get { return _refreshLifetime; }
        set
        {
            if (_refreshLifetime == -1)
                _refreshLifetime = value;
        }
    }
    

    private static SymmetricSecurityKey? _key;
    public static SymmetricSecurityKey Key
    {
        get { return _key; }
        set { _key ??= value; }
    }
}

static class ClaimType
{
    public static readonly string UserId = "UserId";
    public static readonly string Role = "Role";
    public static readonly string TokenType = "TokenType";
    public static readonly string TokenId = "TokenId";
    public static readonly string IssuedByTokenId = "IssuedByTokenId";
}

enum TokenType
{
    Refresh,
    Access,
}

static class Policies
{
    public const string RefreshOnly = "RefreshOnly";
    public const string Admin = "Admin";

}


static class ClaimsExtractor
{
    public static string GetClaimValue(ClaimsPrincipal principal, string ClaimType) =>
        principal.Claims.First(c => c.Type == ClaimType).Value;

    public static ulong GetParentTokenId(ClaimsPrincipal principal) =>
        ulong.Parse(GetClaimValue(principal, ClaimType.IssuedByTokenId));

    public static ulong GetTokenId(ClaimsPrincipal principal) =>
        ulong.Parse(GetClaimValue(principal, ClaimType.TokenId));

    public static Role GetRole(ClaimsPrincipal principal) =>
        Enum.Parse<Role>(GetClaimValue(principal, ClaimType.Role));

    public static Guid GetUserId(ClaimsPrincipal principal) =>
        Guid.Parse(GetClaimValue(principal, ClaimType.UserId));

}