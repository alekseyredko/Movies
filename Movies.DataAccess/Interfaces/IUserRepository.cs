using Movies.Domain.Models;
using System.Threading.Tasks;

namespace Movies.DataAccess.Interfaces
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<User> GetByLoginAsync(string login);
    }
}
