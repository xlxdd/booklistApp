using booklistDomain.Entities;
using booklistDomain.Entities.Identity;
using booklistDomain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace booklistInfrastructure
{
    public class AppDbContext:IdentityDbContext<AppUser,Role,Guid>
    {
        public DbSet<Book> Books { get; private set; }
        public DbSet<BookList> BookList { get; private set; }
        public DbSet<BookCategory> BookCategories { get; private set; }
        public DbSet<Comment> Comments { get; private set; }
        public DbSet<Like> Likes { get; private set; }
        public DbSet<Star> Stars { get; private set; }
        public DbSet<BookBookList> BookBookLists { get; private set; }
        public AppDbContext(DbContextOptions<AppDbContext> opt):base(opt) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
