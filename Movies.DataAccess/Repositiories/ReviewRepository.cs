using Movies.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Models;

namespace Movies.DataAccess.Repositiories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(MoviesDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Review>> GetReviewsWithAllAsync()
        {
            var reviews = await context.Reviews
                .Include(x => x.Movie)
                .Include(x => x.Reviewer).ToListAsync();

            return reviews;
        }

        public async Task<IEnumerable<Review>> GetMovieReviewsAsync(int movieId)
        {
            var reviews = await context.Reviews
                .Where(x => x.MovieId == movieId)
                .ToListAsync();

            return reviews;
        }

        public async Task<IEnumerable<Review>> GetReviewerReviewsAsync(int reviewerId)
        {
            var reviews = await context.Reviews
                .Where(x => x.ReviewerId == reviewerId)
                .ToListAsync();

            return reviews;
        }

        public async Task<Review> GetReviewWithAllAsync(int reviewId)
        {            
            var review = await context.Reviews.SingleAsync(x => x.ReviewerId == reviewId);
            await context.Entry(review).Reference(x => x.Movie).LoadAsync();
            await context.Entry(review).Reference(x => x.Reviewer).LoadAsync();

            return review;
        }

        public async Task<Review> GetReviewWithMovie(int reviewId)
        {
            var review = await context.Reviews.FindAsync(reviewId);
            if(review == null)
            {
                throw new InvalidOperationException($"Review not found in database with id {reviewId}");
            }    

            await context.Entry(review).Reference(x => x.Movie).LoadAsync();

            return review;
        }

        public async Task<Review> GetReviewWithReviewer(int reviewId)
        {
            var review = await context.Reviews.FindAsync(reviewId);
            await context.Entry(review).Reference(x => x.Reviewer).LoadAsync();

            return review;
        }
    }
}
