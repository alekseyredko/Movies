using Movies.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.DataAccess.Interfaces
{
    public interface IReviewerRepository: IGenericRepository<Reviewer>
    {
        Task<IEnumerable<Reviewer>> GetAllReviewersWithAllAsync();
        Task<Reviewer> GetReviewerWithReviewsAsync(int reviewerId);
        Task<Reviewer> GetReviewerWithMoviesAsync(int reviewerId);
        Task<Reviewer> GetFullReviewerAsync(int reviewerId);
        Task<Reviewer> GetByNickNameAsync(string nickname);
    }
}
