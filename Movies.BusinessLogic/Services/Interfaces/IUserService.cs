using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.BusinessLogic.Results;
using Movies.Domain.Models;

namespace Movies.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<User>> RegisterAsync(User userRequest);
        Task<Result<User>> LoginAsync(User request);
        Task<Result<User>> UpdateAccountAsync(User request);
        Task<Result> DeleteAccountAsync(int id);
        Task<IEnumerable<UserRoles>> GetUserRolesAsync(int id);
        Task<Result<User>> GetUserAccountAsync(int id);
    }
}
