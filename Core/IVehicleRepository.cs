using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Db;
using vega.Domain.Models;
using vega.Domain.Models.Vehicle;

namespace vega.Core
{
   public interface IVehicleRepository
    {
        VegaDbContext Context { get; }

        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        Task<List<Vehicle>> GetAll(VehicleQuery filter, bool includeRelated = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}