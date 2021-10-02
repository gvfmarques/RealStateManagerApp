using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.DAL.Mapping
{
    public class RentMapping : IEntityTypeConfiguration<Rent>
    {
        public void Configure(EntityTypeBuilder<Rent> builder)
        {
            builder.HasKey(r => r.RentId);
            builder.Property(r => r.RentValue).IsRequired();
            builder.Property(r => r.MonthId).IsRequired();
            builder.Property(r => r.Year).IsRequired();

            builder.HasOne(r => r.Month).WithMany(r => r.Rents).HasForeignKey(r => r.MonthId);
            builder.HasMany(r => r.Payments).WithOne(r => r.Rent);

            builder.ToTable("Rents");
        }
    }
}
