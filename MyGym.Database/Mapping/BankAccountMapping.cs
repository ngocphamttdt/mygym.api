using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGym.Database.Entities;


namespace MyGym.Database.Mapping
{
    public class BankAccountMapping : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("BankAccount")
                .HasOne<BankBrand>(x => x.BankBrand)
                .WithMany(b => b.BankAccounts)
                .HasForeignKey(b => b.BrandId);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
