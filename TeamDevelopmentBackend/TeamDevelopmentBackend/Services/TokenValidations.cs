using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using TeamDevelopmentBackend.Model;

namespace TeamDevelopmentBackend.Services;

class TokenValidations
{
    public static async Task ValidateTokenParent(TokenValidatedContext context)
    {
        ulong parentTokenId = ClaimsExtractor.GetParentTokenId(context.Principal);
        var dbcontext = context.HttpContext.RequestServices.GetService<DefaultDBContext>();
        var token = new IssuedTokenDbModel {RefreshTokenId = parentTokenId};
        if (!dbcontext.IssuedTokens.Contains(token))
            context.Fail("This token has been revoked!");
    }
}