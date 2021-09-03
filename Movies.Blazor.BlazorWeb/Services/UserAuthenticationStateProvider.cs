using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.IO;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Movies.Blazor.BlazorWeb.Services
{
    public class UserAuthenticationStateProvider : AuthenticationStateProvider
    {        
        private readonly ProtectedLocalStorage _localStorage;        
        public UserAuthenticationStateProvider(ProtectedLocalStorage localStorage)
        {           
            _localStorage = localStorage;        
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {         
            var localStogareVar = await _localStorage.GetAsync<byte[]>("user");

            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            
            if (localStogareVar.Success)
            {
                var buffer = localStogareVar.Value;               

                using (var stream = new MemoryStream(buffer))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        user = new ClaimsPrincipal(reader);
                    }
                }
            }

            return new AuthenticationState(user);
        }
    }
}
