using Movies.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.DataAccess.Interfaces
{
    public interface IActorRepository: IGenericRepository<Actor>
    {        
        Task<Actor> GetActorWithMoviesAsync(int id);
        Task<IEnumerable<Actor>> GetAllActorsWithMoviesAsync();        
    }
}
