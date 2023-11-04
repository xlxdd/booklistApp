using Microsoft.AspNetCore.Identity;

namespace booklistDomain.Entities.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public Role(string name):base(name)
        {
            Id = Guid.NewGuid();
        }
    }
}
