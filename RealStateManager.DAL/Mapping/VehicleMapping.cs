using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.DAL.Mapping
{
    public class VehicleMapping : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(v => v.VehicleId);
            builder.Property(v => v.Name).IsRequired().HasMaxLength(40);
            builder.Property(v => v.Color).IsRequired().HasMaxLength(20);
            builder.Property(v => v.Brand).IsRequired().HasMaxLength(20);
            builder.Property(v => v.Plate).IsRequired().HasMaxLength(20);
            builder.HasIndex(v => v.Plate).IsUnique();
            builder.Property(v => v.UserId).IsRequired();

            builder.HasOne(v => v.User).WithMany(v => v.Vehicles).HasForeignKey(v => v.UserId);

            builder.ToTable("Vehicles");
        }
    }
}
