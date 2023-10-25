using Microsoft.AspNetCore.Identity;

namespace booklistDomain.Entities
{
    public class AppUser: IdentityUser<Guid>
    {
        public IEnumerable<BookList> BookLists { get; private set; }= new List<BookList>();
        public IEnumerable<Comment> comments { get; private set; } = new List<Comment>();
        public AppUser(string userName) : base(userName)
        {
            Id = Guid.NewGuid();
        }
        //暂时这样写
    }
}
