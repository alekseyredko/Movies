using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Interfaces;
using Movies.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.DataAccess.Repositiories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MoviesDBContext context) : base(context)
        {
        }
        
        public async Task<IEnumerable<Person>> GetAllFullPersonAsync()
        {
            var people = await context.People
                .Include(x => x.Actor)
                .Include(x => x.Producer)
                .Include(x => x.Reviewer)
                .ToListAsync();
            return people;
        }

        public async Task<IEnumerable<Person>> GetAllPersonWithActorsAsync()
        {
            var people = await context.People
                .Include(x => x.Actor)
                .ToListAsync();
            return people;
        }
        
        public async Task AddPersonAsync(Person person)
        {
            await context.People.AddAsync(person);
        }


        public async Task<Person> GetPersonWithActorAsync(int id)
        {
            var person = await context.People.SingleAsync(x => x.PersonId == id);
            await context.Entry(person).Reference(x => x.Actor).LoadAsync();
            
            return person;
        }

        public async Task<Person> GetFullPersonAsync(int id)
        {
            var person = await context.People.SingleOrDefaultAsync(x => x.PersonId == id);
            if (person == null)
            {
                return null;
            }
            await context.Entry(person).Reference(x => x.Actor).LoadAsync();
            await context.Entry(person).Reference(x => x.Producer).LoadAsync();
            await context.Entry(person).Reference(x => x.Reviewer).LoadAsync();
            return person;
        }
    }
}
