using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealStateManager.BLL.Models;
using RealStateManager.DAL.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealStateManager.DAL
{
    public class RealStateManagerContext : IdentityDbContext<User, Function, string>
    {
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<HistoryResource> HistoryResources { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<BuildingService>BuildingServices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public RealStateManagerContext(DbContextOptions<RealStateManagerContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RentMapping());
            builder.ApplyConfiguration(new ApartmentMapping());
            builder.ApplyConfiguration(new EventMapping());
            builder.ApplyConfiguration(new FunctionMapping());
            builder.ApplyConfiguration(new MonthMapping());
            builder.ApplyConfiguration(new ServiceMapping());
            builder.ApplyConfiguration(new BuildingServiceMappings());
            builder.ApplyConfiguration(new HistoryResourceMapping());
            builder.ApplyConfiguration(new UserMapping());
            builder.ApplyConfiguration(new VehicleMapping());
        }
    }
}
