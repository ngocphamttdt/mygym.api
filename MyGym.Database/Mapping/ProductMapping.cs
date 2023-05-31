using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGym.Database.Entities;

namespace MyGym.Database.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("Products")
                   .HasOne<Category>(x=>x.Category)
                   .WithMany(p=>p.Products)
                   .HasForeignKey(x=>x.CategoryId)
                 ;
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
