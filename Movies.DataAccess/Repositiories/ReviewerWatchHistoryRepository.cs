using Movies.DataAccess.Interfaces;
using Movies.Domain.Models;

namespace Movies.DataAccess.Repositiories
{
    public class ReviewerWatchHistoryRepository : GenericRepository<ReviewerWatchHistory>, IReviewerWatchHistoryRepository
    {
        public ReviewerWatchHistoryRepository(MoviesDBContext context) : base(context)
        {
        }
    }
}