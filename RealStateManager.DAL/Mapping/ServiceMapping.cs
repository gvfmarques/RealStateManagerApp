using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.DAL.Mapping
{
    public class ServiceMapping : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(r => r.ServiceId);
            builder.Property(r => r.Name).IsRequired().HasMaxLength(30);
            builder.Property(r => r.Value).IsRequired();
            builder.Property(r => r.Status).IsRequired();
            builder.Property(r => r.UserId).IsRequired();

            builder.HasOne(s => s.User).WithMany(s => s.Services).HasForeignKey(s => s.UserId);
            builder.HasMany(s => s.BuildingServices).WithOne(s => s.Service);

            builder.ToTable("Services");
        }
    }
}
