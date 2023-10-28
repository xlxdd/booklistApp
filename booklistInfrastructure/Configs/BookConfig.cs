using booklistDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace booklistInfrastructure.Configs
{
    public class BookConfig:IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("T_Books");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.BookName).IsUnicode(true).HasMaxLength(30);
            builder.Property(e => e.Author).IsUnicode(true).HasMaxLength(30);
            builder.Property(e => e.Price).HasColumnType("decimal(5, 2)");
            builder.Property(e => e.Abs).IsUnicode(true).HasMaxLength(200);
        }
    }
}
