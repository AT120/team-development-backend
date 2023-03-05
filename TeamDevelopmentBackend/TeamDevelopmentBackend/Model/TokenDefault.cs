using System.Reflection.Metadata.Ecma335;
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

    private static int _lifetime = -1;
    public static int Lifetime 
    {
        get { return _lifetime; }
        set
        {
            if (_lifetime == -1)
                _lifetime = value;
        }
    }

    private static SymmetricSecurityKey? _key;
    public static SymmetricSecurityKey Key
    {
        get { return _key; }
        set { _key ??= value; }
    }
}

class ClaimsDefault
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

