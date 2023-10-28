using booklistDomain.Entities;
using booklistDomain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace booklistInfrastructure.Configs
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("T_Comments");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Content).IsUnicode(true).HasMaxLength(200);
        }
    }
}
