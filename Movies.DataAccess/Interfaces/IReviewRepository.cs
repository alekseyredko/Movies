using Movies.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.DataAccess.Interfaces
{
    public interface IReviewRepository: IGenericRepository<Review>
    {        
        Task<Review> GetReviewWithMovie(int reviewId);
        Task<Review> GetReviewWithReviewer(int reviewId);
        Task<Review> GetReviewWithAllAsync(int reviewId);
        Task<IEnumerable<Review>> GetReviewsWithAllAsync();
        Task<IEnumerable<Review>> GetMovieReviewsAsync(int movieId);
        Task<IEnumerable<Review>> GetReviewerReviewsAsync(int reviewerId);
    }
}
