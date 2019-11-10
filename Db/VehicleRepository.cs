using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Domain.Models.Vehicle;

namespace vega.Db
{
    public class VehicleRepository : IVehicleRepository
    {
        public VegaDbContext Context { get; }
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await Context.Vehicles.FindAsync(id);

            var vehicle = await Context.Vehicles
                .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .Include(v => v.VehicleModel)
                .SingleOrDefaultAsync(v => v.Id == id);

            return vehicle;
        }

        public VehicleRepository(VegaDbContext context)
        {
            this.Context = context;
        }

        public void Add(Vehicle vehicle)
        {
            Context.Vehicles.Add(vehicle);
        }
        public void Remove(Vehicle vehicle)
        {
            Context.Vehicles.Remove(vehicle);
        }
    }
}