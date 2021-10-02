using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.DAL.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Identification).IsRequired().HasMaxLength(30);
            builder.HasIndex(u => u.Identification).IsUnique();
            builder.Property(u => u.Picture).IsRequired();
            builder.Property(u => u.FirstAccess).IsRequired();
            builder.Property(u => u.Status).IsRequired();

            builder.HasMany(u => u.ApartmentOwners).WithOne(u => u.ApartmentOwner);
            builder.HasMany(u => u.ApartmentResidents).WithOne(u => u.ApartmentResident);
            builder.HasMany(u => u.Vehicles).WithOne(u => u.User);
            builder.HasMany(u => u.Events).WithOne(u => u.User);
            builder.HasMany(u => u.Payments).WithOne(u => u.User);
            builder.HasMany(u => u.Services).WithOne(u => u.User);

            builder.ToTable("Users");
        }
    }
}
