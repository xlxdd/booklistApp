using booklistDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace booklistInfrastructure.Configs
{
    public class BookListConfig : IEntityTypeConfiguration<BookList>
    {
        public void Configure(EntityTypeBuilder<BookList> builder)
        {
            builder.ToTable("T_BookLists");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Title).IsUnicode(true).HasMaxLength(20);
            builder.Property(e => e.Descrpition).IsUnicode(true).HasMaxLength(200);
            builder.HasIndex(e => new { e.IsDeleted, e.CreaterId });
        }
    }
}
