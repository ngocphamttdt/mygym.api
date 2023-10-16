using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGym.Database.Entities;


namespace MyGym.Database.Mapping
{
    public class LoginModelMapping : IEntityTypeConfiguration<LoginModel>
    {
        public void Configure(EntityTypeBuilder<LoginModel> builder)
        {
            builder.ToTable("LoginModel");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
          
        }
    }
}
