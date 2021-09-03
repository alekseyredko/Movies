
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
        //public static IEnumerable<Claim> GetClaimsFromJwt(string token)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    var decodedToken = tokenHandler.ReadJwtToken(token);            
        //    return decodedToken.Claims;
        //}

        public static IEnumerable<Claim> GetClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];

            var jsonBytes = ParseBase64WithoutPadding(payload);
            
            var keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, object>>(Encoding.UTF8.GetString(jsonBytes));

            foreach (var item in keyValuePairs)
            {
                if (item.Value is Newtonsoft.Json.Linq.JArray)
                {
                    var array = item.Value as Newtonsoft.Json.Linq.JArray;
                    foreach (var arrayItem in array)
                    {
                        claims.Add(new Claim(item.Key, arrayItem.ToString()));
                    }
                    continue;
                }

                if (item.Key == "sub")
                {
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, item.Value.ToString()));
                    continue;
                }
                claims.Add(new Claim(item.Key, item.Value.ToString()));
            }

            //claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
