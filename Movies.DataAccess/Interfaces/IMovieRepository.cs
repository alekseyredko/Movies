using Movies.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.DataAccess.Interfaces
{
    public interface IMovieRepository: IGenericRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetMoviesWithAllAsync();
        Task<IEnumerable<Movie>> GetMoviesByProducerIdAsync(int producerId);
        Task<IEnumerable<Movie>> GetMoviesWithActorsAsync();        
        Task<Movie> GetMovieWithActorsAsync(int movieId);
        Task<Movie> GetMovieWithAllAsync(int movieId);
        Task<Movie> GetMovieWithReviewsAsync(int movieId);
        Task<Movie> GetMovieWithReviewersAsync(int movieId);
        Task<Movie> GetMovieByNameAsync(string movieName);
    }
}