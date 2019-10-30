using Microsoft.EntityFrameworkCore;

namespace vega.Db
{
    public class VegaDbContext: DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options): base(options) 
        {

        }
    }
}