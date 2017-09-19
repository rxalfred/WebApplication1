using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication2.Providers
{
    public class AdminHandler : AuthorizationHandler<AdminRequirement>
    {
        const string Issuer = "http://contoso.com";
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role &&
                                       c.Issuer == Issuer))
            {
                context.Fail();
                // .NET 4.x -> return Task.FromResult(0);
                return Task.CompletedTask;
            }

            var userRole = context.User.FindFirst(c => c.Type == ClaimTypes.Role && c.Issuer == Issuer).Value;

            if (userRole == requirement.IsAdmin.ToString())
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}