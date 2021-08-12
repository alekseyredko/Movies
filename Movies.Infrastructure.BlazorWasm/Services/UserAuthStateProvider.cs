using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace Movies.Infrastructure.BlazorWasm.Services
{
    public class UserAuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;
        private readonly IJSRuntime jSRuntime;       

        public UserAuthStateProvider(HttpClient httpClient, IJSRuntime jSRuntime)
        {
            this.httpClient = httpClient;
            this.jSRuntime = jSRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {           
            var token = await jSRuntime.InvokeAsync<string>("getCookie", "Token");
            var identity = new ClaimsIdentity();

            if (!string.IsNullOrWhiteSpace(token))
            {
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var claims = JwtClaimsExtractor.GetClaimsFromJwt(token);

                identity = new ClaimsIdentity(claims, "custom");
            }
                                   
            var user = new ClaimsPrincipal(identity);            
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));            
            return state;
        }
    }
}
