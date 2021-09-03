using Movies.DataAccess.Interfaces;
using Movies.Domain.Models;

namespace Movies.DataAccess.Repositiories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MoviesDBContext context) : base(context)
        {
        }
    }
}