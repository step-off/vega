using System.Threading.Tasks;

namespace vega.Db
{
    public class UnitOfWork : IUnitOfWork
    {
        public VegaDbContext Context { get; }
        public async Task CompleteAsync()
        {
            await Context.SaveChangesAsync();
        }

        public UnitOfWork(VegaDbContext context)
        {
            this.Context = context;

        }
    }
}