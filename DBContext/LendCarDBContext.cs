using LendCar.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.DBContext
{
    public class LendCarDBContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Vehicle>()
                   .HasIndex(v => v.VIN)
                   .IsUnique();
            
            builder.Entity<ApplicationUser>()
                   .HasIndex(u => u.NationalId)
                   .IsUnique();

            builder.Entity<Brand>()
                   .HasIndex(b => b.Name)
                   .IsUnique();

            builder.Entity<BrandModel>()
                   .HasIndex(bm => new { bm.Name, bm.BrandId})
                   .IsUnique();

            builder.Entity<City>()
                   .HasIndex(c => c.Name)
                   .IsUnique();

            builder.Entity<Color>()
                   .HasIndex(c => c.Name)
                   .IsUnique();

            builder.Entity<VehicleType>()
                   .HasIndex(vt => vt.Type)
                   .IsUnique();
        }
        public LendCarDBContext(DbContextOptions<LendCarDBContext> options) : base(options)  { }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<OdoMeter> OdoMeters { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandModel> BrandModels { get; set; }
        public DbSet<VehicleBooking> VehicleBookings { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<City> Cities { get; set; }

    }
}
