using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.DAL.Mapping
{
    public class HistoryResourceMapping : IEntityTypeConfiguration<HistoricResource>
    {
        public void Configure(EntityTypeBuilder<HistoricResource> builder)
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
