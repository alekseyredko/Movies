using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Movies.BusinessLogic.Services.Interfaces;
using Movies.Domain.Models;

namespace Movies.Infrastructure.Services
{
    public class CustomAuthorizationMiddlewareHandler: IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler
            DefaultHandler = new AuthorizationMiddlewareResultHandler();

        private readonly IUserService _userService;

        public CustomAuthorizationMiddlewareHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(
            RequestDelegate requestDelegate,
            HttpContext httpContext,
            AuthorizationPolicy authorizationPolicy,
            PolicyAuthorizationResult policyAuthorizationResult)
        {
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                httpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                return;
            }

            var id = RefreshTokenService.GetIdFromToken(httpContext);
            var roles = await _userService.GetUserRolesAsync(id);

            var claims = httpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role);

            foreach (var userClaim in claims)
            {
                if (!roles.Contains(Enum.Parse<UserRoles>(userClaim.Value)))
                {
                    httpContext.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                    return;
                }
            }

            // Fallback to the default implementation.
            await DefaultHandler.HandleAsync(requestDelegate, httpContext, authorizationPolicy,
                policyAuthorizationResult);
        }
    }
}
