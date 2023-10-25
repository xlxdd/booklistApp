using Microsoft.AspNetCore.Identity;

namespace booklistDomain.Entities.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public Role()
        {
            Id = Guid.NewGuid();
        }
    }
}
