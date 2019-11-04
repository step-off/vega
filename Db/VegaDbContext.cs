using Microsoft.EntityFrameworkCore;
using vega.Domain.Models;

namespace vega.Db
{
    public class VegaDbContext: DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options): base(options) 
        {

        }

        public DbSet<VehicleMake> Makes { get; set; }
    }
}