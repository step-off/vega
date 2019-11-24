using Microsoft.EntityFrameworkCore;
using vega.Domain.Models;
using vega.Domain.Models.Features;
using vega.Domain.Models.Vehicle;

namespace vega.Db
{
    public class VegaDbContext: DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleMake> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public VegaDbContext(DbContextOptions<VegaDbContext> options): base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<VehicleFeature>().HasKey(i => new { i.FeatureId, i.VehicleId });
        }
    }
}