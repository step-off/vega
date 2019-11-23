using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Core;
using vega.Domain.Models;
using vega.Domain.Models.Vehicle;
using vega.Extensions;

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
        public async Task<List<Vehicle>> GetAll(VehicleQuery queryObj, bool includeRelated = true) 
        {
            var query = Context.Vehicles
                .Include(v => v.VehicleModel)
                    .ThenInclude(m => m.Make)
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .AsQueryable();

            var sortingDictionary = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.VehicleModel.Make.Name,
                ["name"] = v => v.Name
            };
            if (queryObj.MakeId.HasValue) {
                query = query.Where(v => v.VehicleModel.MakeId == queryObj.MakeId);
            }    
            query = query.ApplyOrdering(queryObj, sortingDictionary);

            query = query.ApplyPaging(queryObj);
            
            var vehiclesList = await query.ToListAsync();

            return vehiclesList;   
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