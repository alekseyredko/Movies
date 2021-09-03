using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Movies.Blazor.BlazorWeb.Services;
using Movies.Infrastructure.Services.Interfaces;

namespace Movies.Blazor.BlazorWeb.Extensions
{
    public static class BlazorWebServerAuthExtensions
    {
        public static void AddBlazorWebServerAuth(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationStateProvider, UserAuthenticationStateProvider>();
            
            services.AddScoped<ICustomAuthentication, CustomAuthentication>();

            services.AddAuthorizationCore();
        }
    }       
}
