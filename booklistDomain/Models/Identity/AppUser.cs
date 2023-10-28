using booklistDomain.Models;
using Microsoft.AspNetCore.Identity;

namespace booklistDomain.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser(string userName) : base(userName)
        {
            Id = Guid.NewGuid();
        }
        //暂时这样写
    }
}
