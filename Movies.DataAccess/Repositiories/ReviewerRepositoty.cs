using Movies.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Models;

namespace Movies.DataAccess.Repositiories
{
    public class ReviewerRepositoty: GenericRepository<Reviewer>, IReviewerRepository
    {

        public ReviewerRepositoty(MoviesDBContext moviesDBContext): base(moviesDBContext)
        {

        }

        public async Task<IEnumerable<Reviewer>> GetAllReviewersWithAllAsync()
        {
            var reviewers = await context.Reviewers
                .Include(x => x.Reviews)
                .Include(x => x.Movies)
                .ToListAsync();

            return reviewers;
        }

        public async Task<Reviewer> GetFullReviewerAsync(int reviewerId)
        {
            var reviewer = await context.Reviewers.FindAsync(reviewerId);

            if (reviewer == null)
            {
                return null;
            }

            await context.Entry(reviewer).Collection(x => x.Movies).LoadAsync();
            await context.Entry(reviewer).Collection(x => x.Reviews).LoadAsync();

            return reviewer;
        }

        public Task<Reviewer> GetByNickNameAsync(string nickname)
        {
            return context.Reviewers.FirstOrDefaultAsync(x => string.Equals(x.NickName, nickname, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Reviewer> GetReviewerWithMoviesAsync(int reviewerId)
        {
            var reviewer = await context.Reviewers.FindAsync(reviewerId);
            await context.Entry(reviewer).Collection(x => x.Movies).LoadAsync();            

            return reviewer;
        }

        public async Task<Reviewer> GetReviewerWithReviewsAsync(int reviewerId)
        {
            var reviewer = await context.Reviewers.FindAsync(reviewerId);

            if (reviewer == null)
            {
                return null;
            }

            await context.Entry(reviewer).Collection(x => x.Reviews).LoadAsync();

            return reviewer;
        }
    }
}
