using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vega.Controllers.Resources;
using vega.Core;
using vega.Domain.Models;
using vega.Domain.Models.Vehicle;

namespace vega.Controllers
{
    [Route("api/vehicles")]
    public class VehiclesController : Controller
    {
        public IMapper Mapper { get; set; }
        public IVehicleRepository VehicleRepository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
        public VehiclesController( 
            IMapper mapper, 
            IVehicleRepository vehicleRepository, 
            IUnitOfWork unitOfWork
            )
        {
            this.UnitOfWork = unitOfWork;
            this.VehicleRepository = vehicleRepository;
            this.Mapper = mapper;
        }
        [HttpGet]
        public async Task<List<VehicleResource>> GetFeatures(VehicleQueryResource filterResource)
        {
            var filter = Mapper.Map<VehicleQueryResource, VehicleQuery>(filterResource);
            var vehicles = await VehicleRepository.GetAll(filter);
            return Mapper.Map<List<Vehicle>, List<VehicleResource>>(vehicles);
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = Mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            VehicleRepository.Add(vehicle);
            await UnitOfWork.CompleteAsync();

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

            await UnitOfWork.CompleteAsync();

            vehicle = await VehicleRepository.GetVehicle(vehicle.Id);
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
            await UnitOfWork.CompleteAsync();

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