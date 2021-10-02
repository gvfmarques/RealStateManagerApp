using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.DAL.Mapping
{
    public class BuildingServiceMappings : IEntityTypeConfiguration<BuildingService>
    {
        public void Configure(EntityTypeBuilder<BuildingService> builder)
        {
            builder.HasKey(bs => bs.BuildingServiceId);
            builder.Property(bs => bs.ServiceId).IsRequired();
            builder.Property(bs => bs.ExecutionDate).IsRequired();

            builder.HasOne(bs => bs.Service).WithMany(bs => bs.BuildingServices).HasForeignKey(bs => bs.ServiceId);

            builder.ToTable("BuildingServices");
        }
    }
}
