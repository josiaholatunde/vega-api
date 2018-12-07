using Microsoft.EntityFrameworkCore;
using VegaAPI.Models;

namespace VegaAPI.Context
{
    public class VegaContext : DbContext
    {
        public VegaContext(DbContextOptions options) : base(options)
        {}
        public DbSet<Make> Makes {get;set;}
        public DbSet<Feature> Feature {get;set;}
        public DbSet<Vehicle> Vehicles {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>().HasKey(vf => new { vf.FeatureId, vf.VehicleId });
        }
        
    }
    
}