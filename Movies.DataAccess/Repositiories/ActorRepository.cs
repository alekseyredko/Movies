using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Interfaces;
using Movies.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.DataAccess.Repositiories
{
    public class ActorRepository : GenericRepository<Actor>, IActorRepository
    {
        public ActorRepository(MoviesDBContext context) : base(context)
        {
              
        }

        public async Task<Actor> GetActorWithMoviesAsync(int id)
        {
            var actor = await context.Actors.SingleAsync(x => x.ActorId == id);
            await context.Entry(actor).Collection(x => x.Movies).LoadAsync();

            return actor;
        }

        public async Task<IEnumerable<Actor>> GetAllActorsWithMoviesAsync()
        {
            var actors = await context.Actors.Include(x => x.Movies).ToListAsync();
            return actors;
        }
    }
}
