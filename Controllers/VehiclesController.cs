using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Db;
using vega.Domain.Models.Vehicle;

namespace vega.Controllers
{
    [Route("api/vehicles")]
    public class VehiclesController : Controller
    {
        public IMapper Mapper { get; set; }
        public VegaDbContext Context { get; set; }
        public VehiclesController(VegaDbContext context, IMapper mapper)
        {
            this.Context = context;
            this.Mapper = mapper;
        }
        [HttpPost]
        public IActionResult CreateVehicle([FromBody] VehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = Mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            Context.Vehicles.Add(vehicle);
            Context.SaveChanges();

            var result = Mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] VehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await Context.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);
            if (vehicle == null)
                return NotFound();

            Mapper.Map<VehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            Context.SaveChanges();

            var result = Mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id) 
        {
            var vehicle = await Context.Vehicles.FindAsync(id);

            if (vehicle == null)
                return NotFound();

            Context.Remove(vehicle);
            await Context.SaveChangesAsync(); 

            return Ok(id);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await Context.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);
            if (vehicle == null)
                return NotFound();
        
            var result = Mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }
    }
}