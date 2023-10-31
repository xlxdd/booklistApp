using booklistDomain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace booklistInfrastructure.Configs
{
    public class BookBookListConfig : IEntityTypeConfiguration<BookBookList>
    {
        public void Configure(EntityTypeBuilder<BookBookList> builder)
        {
            builder.ToTable("T_BookBookList");
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => new { e.BookId, e.BookListId });
        }
    }
}
