using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.DAL.Mapping
{
    public class ApartmentMapping : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.HasKey(a => a.ApartmentId);
            builder.Property(a => a.Number).IsRequired();
            builder.Property(a => a.Floor).IsRequired();
            builder.Property(a => a.Picture).IsRequired();
            builder.Property(a => a.ApartmentOwnerId).IsRequired();
            builder.Property(a => a.ApartmentResidentId).IsRequired(false);

            builder.HasOne(a => a.ApartmentOwner).WithMany(a => a.ApartmentOwners).HasForeignKey(a => a.ApartmentOwnerId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(a => a.ApartmentResident).WithMany(a => a.ApartmentResidents).HasForeignKey(a => a.ApartmentResidentId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Apartments");
        }
    }
}
