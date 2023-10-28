using booklistDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace booklistInfrastructure.Configs
{
    public class BookCategoryConfig:IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.ToTable("T_BookCatogories");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsUnicode(true).HasMaxLength(10);
        }
    }
}
