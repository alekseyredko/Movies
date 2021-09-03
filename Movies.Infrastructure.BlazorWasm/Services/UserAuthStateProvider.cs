using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Movies.Infrastructure.BlazorWasm.Services.Interfaces;

namespace Movies.Infrastructure.BlazorWasm.Services
{
    public class UserAuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;
        private readonly IJSRuntime jSRuntime;
        private readonly IHttpResultService httpResultService;

        public UserAuthStateProvider(HttpClient httpClient, IJSRuntime jSRuntime, IHttpResultService httpResultService)
        {
            this.httpClient = httpClient;
            this.jSRuntime = jSRuntime;
            this.httpResultService = httpResultService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {                       
            var identity = new ClaimsIdentity();
            IEnumerable<Claim> claims = new List<Claim>();

            var token = await jSRuntime.InvokeAsync<string>("getCookie", "Token");

            if (!string.IsNullOrWhiteSpace(token))
            {
                claims = JwtClaimsExtractor.GetClaimsFromJwt(token);
                identity = new ClaimsIdentity(claims, "custom");
            }
            else
            {
                var result = await httpResultService.TryRefreshTokenAsync();

                if (result.ResultType == BusinessLogic.Results.ResultType.Ok)
                {
                    token = await jSRuntime.InvokeAsync<string>("getCookie", "Token");
                    claims = JwtClaimsExtractor.GetClaimsFromJwt(token);
                    identity = new ClaimsIdentity(claims, "custom");
                }
            }
                 
            var user = new ClaimsPrincipal(identity);            

            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        public void Notify()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
