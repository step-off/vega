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
        public IVehicleRepository VehicleRepository { get; set; }
        public VehiclesController(VegaDbContext context, IMapper mapper, IVehicleRepository vehicleRepository)
        {
            this.VehicleRepository = vehicleRepository;
            this.Context = context;
            this.Mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = Mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            VehicleRepository.Add(vehicle);
            Context.SaveChanges();

            vehicle = await VehicleRepository.GetVehicle(vehicle.Id);
            var result = Mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await VehicleRepository.GetVehicle(id);
            if (vehicle == null)
                return NotFound();

            Mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            Context.SaveChanges();

            var result = Mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await VehicleRepository.GetVehicle(id, includeRelated: false);

            if (vehicle == null)
                return NotFound();

            VehicleRepository.Remove(vehicle);
            await Context.SaveChangesAsync();

            return Ok(id);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await VehicleRepository.GetVehicle(id);
            if (vehicle == null)
                return NotFound();

            var result = Mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }
    }
}