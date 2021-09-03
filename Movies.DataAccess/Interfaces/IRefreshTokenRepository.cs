using Movies.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.DataAccess.Interfaces
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken> GetRefreshTokenByTokenValue(string token);
        Task<IEnumerable<RefreshToken>> GetRefreshTokensByUserId(int userId);
        Task<User> GetUserByRefreshToken(int tokenId);
        Task SetAllUserTokensRevoked(int userId);
    }
}
