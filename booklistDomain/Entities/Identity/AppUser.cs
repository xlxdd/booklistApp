using Microsoft.AspNetCore.Identity;

namespace booklistDomain.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public IEnumerable<BookList> BookLists { get; private set; } = new List<BookList>();
        public IEnumerable<Comment> Comments { get; private set; } = new List<Comment>();
        public IEnumerable<Star> Stars { get; private set; } = new List<Star>();
        public AppUser(string userName) : base(userName)
        {
            Id = Guid.NewGuid();
        }
        //暂时这样写
    }
}
