using System.Collections.Generic;
using Movies.Domain.Models;

namespace Movies.Infrastructure.Models.User
{
    public class GetUserResponse
    {
        public int UserId { get; set; }

        public string Login { get; set; }

        public ICollection<UserRoles> Roles { get; set; }
    }
}
