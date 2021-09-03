using Movies.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Models;

namespace Movies.DataAccess.Repositiories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MoviesDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Movie>> GetMoviesWithActorsAsync()
        {
            var movies = await context.Movies
                .Include(x => x.Actors)
                .ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetMoviesWithAllAsync()
        {
            var movies = await context.Movies
                .Include(x => x.Actors)
                .Include(x => x.Producer)
                .Include(x => x.Reviewers)
                .Include(x => x.Reviews)
                .Include(x => x.ReviewerWatchHistories)
                .ToListAsync();

            return movies;
        }

        public async Task<Movie> GetMovieWithActorsAsync(int movieId)
        {
            var movie = await context.Movies.FindAsync(movieId);

            if (movie == null)
            {
                return null;
            }

            await context.Entry(movie).Collection(x => x.Actors).LoadAsync();

            return movie;
        }

        public async Task<Movie> GetMovieWithAllAsync(int movieId)
        {
            var movie = await context.Movies.FirstAsync(x => x.MovieId == movieId);
            foreach (var item in context.Entry(movie).Collections)
            {
                await item.LoadAsync();
            }
            await context.Entry(movie).Reference(x => x.Producer).LoadAsync();

            return movie;
        }

        public async Task<Movie> GetMovieWithReviewsAsync(int movieId)
        {
            var movie = await context.Movies.FirstAsync(x => x.MovieId == movieId);

            if (movie == null)
            {
                return null;
            }

            await context.Entry(movie).Collection(x => x.Reviews).LoadAsync();

            return movie;
        }

        public async Task<Movie> GetMovieWithReviewersAsync(int movieId)
        {
            var movie = await context.Movies.FirstAsync(x => x.MovieId == movieId);

            if (movie == null)
            {
                return null;
            }

            await context.Entry(movie).Collection(x => x.Reviewers).LoadAsync();

            return movie;
        }

        public Task<Movie> GetMovieByNameAsync(string movieName)
        {
            return context.Movies.FirstOrDefaultAsync(x => x.MovieName.ToLower() == movieName.ToLower());
        }

        public async Task<IEnumerable<Movie>> GetMoviesByProducerIdAsync(int producerId)
        {
            var movies = await context.Movies.Where(x => x.ProducerId == producerId).ToListAsync();

            return movies;
        }
    }
}
