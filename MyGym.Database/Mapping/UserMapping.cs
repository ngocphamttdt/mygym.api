using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGym.Database.Entities;

namespace MyGym.Database.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.ToTable("Users")
                   .HasMany(u => u.Products)
                   .WithOne(u => u.User)
                   .HasForeignKey(u => u.UserId )
                 ;
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
