using System.Threading.Tasks;
using vega.Domain.Models.Vehicle;

namespace vega.Db
{
   public interface IVehicleRepository
    {
        VegaDbContext Context { get; }

        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}