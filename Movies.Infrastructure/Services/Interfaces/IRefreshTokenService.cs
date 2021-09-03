using Microsoft.AspNetCore.Http;
using Movies.BusinessLogic.Results;
using Movies.Domain.Models;
using Movies.Infrastructure.Authentication;
using Movies.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<Result> DeleteCookiesFromClient(HttpRequest request, HttpResponse response);
        Task<Result> GenerateAndWriteTokensToResponseAsync(int id, HttpResponse response);
        string GenerateJWTAsync(int id, AuthConfiguration authConfiguration, IEnumerable<UserRoles> roles = null);
        RefreshToken GenerateRefreshToken(AuthConfiguration authConfiguration);
        Task<Result<TokenResponse>> GenerateTokenPairAsync(int id);
        Task<Result> RefreshTokenAsync(string token, HttpResponse response);
    }
}