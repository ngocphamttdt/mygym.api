using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGym.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGym.Database.Mapping
{
    public class BankBrandMapping : IEntityTypeConfiguration<BankBrand>
    {
        public void Configure(EntityTypeBuilder<BankBrand> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("BankBrand");

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
