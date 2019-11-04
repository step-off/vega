using AutoMapper;
using vega.Controllers.Resources;
using vega.Domain.Models;

namespace vega.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleMake, VehicleMakeResource>();
            CreateMap<VehicleModel, VehicleModelResource>();
        }
    }
}