using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealStateManager.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.DAL.Mapping
{
    class EventMapping : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.EventId);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Date).IsRequired();
            builder.Property(e => e.UserId).IsRequired();

            builder.HasOne(e => e.User).WithMany(e => e.Events).HasForeignKey(e => e.UserId);

            builder.ToTable("Events");
        }
    }
}
