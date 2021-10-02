using RealStateManager.BLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealStateManager.DAL.Mapping
{
    public class FunctionMapping : IEntityTypeConfiguration<Function>
    {
        public void Configure(EntityTypeBuilder<Function> builder)
        {
            builder.Property(f => f.Id).ValueGeneratedOnAdd();
            builder.Property(f => f.Description).IsRequired().HasMaxLength(30);

            builder.HasData(
                new Function
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Resident",
                    NormalizedName = "RESIDENT",
                    Description = "Apartment Resident"
                },

                new Function
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "ResidentManager",
                    NormalizedName = "RESIDENT MANAGER ",
                    Description = "Building Resident Manager"
                },

                new Function
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Manager",
                    NormalizedName = "MANAGER",
                    Description = "Building Manager"
                });
            builder.ToTable("Functions");
        }
    }
}
