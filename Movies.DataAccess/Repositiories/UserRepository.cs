using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Interfaces;
using Movies.Domain.Models;

namespace Movies.DataAccess.Repositiories
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        public UserRepository(MoviesDBContext context) : base(context)
        {
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Login.ToLower() == login.ToLower());
            return user;
        }
    }
}
