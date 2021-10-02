using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.DAL.Mapping
{
    public class HistoryResourceMapping : IEntityTypeConfiguration<HistoryResource>
    {
        public void Configure(EntityTypeBuilder<HistoryResource> builder)
        {
            builder.HasKey(hr => hr.HistoryResourceId);
            builder.Property(hr => hr.Value).IsRequired();
            builder.Property(hr => hr.Type).IsRequired();
            builder.Property(hr => hr.Day).IsRequired();
            builder.Property(hr => hr.MonthId).IsRequired();
            builder.Property(hr => hr.Year).IsRequired();

            builder.HasOne(hr => hr.Month).WithMany(hr => hr.HistoryResources).HasForeignKey(hr => hr.MonthId);

            builder.ToTable("HistoryResources");
        }
    }
}
