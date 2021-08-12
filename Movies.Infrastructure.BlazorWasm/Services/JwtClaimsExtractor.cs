using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.BlazorWasm.Services
{
    public class JwtClaimsExtractor
    {
        public static IEnumerable<Claim> GetClaimsFromJwt(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var decodedToken = tokenHandler.ReadJwtToken(token);

            return decodedToken.Claims;
        }
    }
}
