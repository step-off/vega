using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using vega.Controllers.Resources;
using vega.Core;
using vega.Domain.Models.Vehicle;

namespace vega.Controllers
{
    [Route("api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        public IHostingEnvironment Environment { get; }
        public IMapper Mapper { get; }
        public IUnitOfWork UnitOfWork { get; }
        public IVehicleRepository VehicleRepository { get; }
        public PhotoSettings PhotoSettings { get; }

        public PhotosController(
            IHostingEnvironment environment,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IVehicleRepository vehicleRepository,
            IOptionsSnapshot<PhotoSettings> photoSettings
        ) {
            this.Environment = environment;
            this.Mapper = mapper;
            this.UnitOfWork = unitOfWork;
            this.VehicleRepository = vehicleRepository;
            this.PhotoSettings = photoSettings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Post(int vehicleId, IFormFile file)
        {
            var vehicle =  await VehicleRepository.GetVehicle(vehicleId);
            if (vehicle == null)
                return NotFound();
            if (file == null) return BadRequest("File is null");
            if (file.Length == 0) return BadRequest("Empty file");
            if (!PhotoSettings.IsValid(file)) return BadRequest("Invalid file");

            var uploadingFolderPath = Path.Combine(Environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadingFolderPath)) {
                Directory.CreateDirectory(uploadingFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadingFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var photo = new Photo() {Name = fileName};
            vehicle.Photos.Add(photo);

            await UnitOfWork.CompleteAsync();
            var result = Mapper.Map<Photo, PhotoResource>(photo);
            return Ok(result);
        }
    }
}