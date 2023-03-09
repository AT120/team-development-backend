using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TeamDevelopmentBackend.Model;

namespace TeamDevelopmentBackend.Services;

public class ValidatorsPile
{
    public static Task ValidateTokenParent(TokenValidatedContext context)
    {
        ulong parentTokenId = ClaimsExtractor.GetParentTokenId(context.Principal);
        var dbcontext = context.HttpContext.RequestServices.GetService<DefaultDBContext>();
        var token = new IssuedTokenDbModel {RefreshTokenId = parentTokenId};
        if (!dbcontext.IssuedTokens.Contains(token)) 
            context.Fail("This token has been revoked!");
        
        return Task.CompletedTask;
                     
    }

    public static bool ValidateAdminRole(AuthorizationHandlerContext context)
    {
        if (!context.User.Identity.IsAuthenticated)
            return false;

        var httpcontext = context.Resource as HttpContext;
        if (httpcontext is null) { return false; }

        var dbcontext = httpcontext.RequestServices.GetService<DefaultDBContext>();

        Guid userId = ClaimsExtractor.GetUserId(context.User);
        var role = dbcontext.Users.Find(userId)?.Role;
        
        return role is not null && role == Role.Admin;
    }
}