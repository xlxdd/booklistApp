using booklistDomain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace booklistInfrastructure.Configs
{
    public class BookBookCategoryConfig : IEntityTypeConfiguration<BookBookCategory>
    {
        public void Configure(EntityTypeBuilder<BookBookCategory> builder)
        {
            builder.ToTable("BookBookCTGR");
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => new { e.BookId, e.BookCategoryId });
        }
    }
}
