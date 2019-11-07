using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    }
}