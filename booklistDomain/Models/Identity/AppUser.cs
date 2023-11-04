using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;

namespace booklistDomain.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser() : base( )
        {
            Id = Guid.NewGuid();
        }
        public static AppUser Create(string phone)
        {
            var user = new AppUser();
            user.UserName = phone;
            user.PhoneNumber = phone;
            return user;
        }
    }
}
