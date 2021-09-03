using Movies.DataAccess.Interfaces;
using Movies.Domain.Models;

namespace Movies.DataAccess.Repositiories
{
    public class ProducerRepository: GenericRepository<Producer>, IProducerRepository
    {
        public ProducerRepository(MoviesDBContext context) : base(context)
        {
        }
    }
}
