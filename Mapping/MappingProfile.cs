using System.Linq;
using AutoMapper;
using vega.Controllers.Resources;
using vega.Domain.Models;
using vega.Domain.Models.Vehicle;

namespace vega.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API
            CreateMap<VehicleMake, VehicleMakeResource>();
            CreateMap<VehicleModel, VehicleModelResource>();
             CreateMap<Vehicle, VehicleResource>()
             .ForMember(v => v.Contact, opt => opt.MapFrom(resource => new ContactResource { 
                 Name = resource.ContactName, 
                 Email = resource.ContactEmail,
                 Phone = resource.ContactPhone 
            }))
            .ForMember(v => v.Features, opt => opt.MapFrom(resource => resource.Features.Select(i => i.FeatureId)));

            // API to Domain
            CreateMap<VehicleResource, Vehicle>()
            .ForMember(v => v.ContactName, opt => opt.MapFrom(resource => resource.Contact.Name))
            .ForMember(v => v.ContactEmail, opt => opt.MapFrom(resource => resource.Contact.Email))
            .ForMember(v => v.ContactPhone, opt => opt.MapFrom(resource => resource.Contact.Phone))
            .ForMember(v => v.Features, opt => opt.MapFrom(resource => resource.Features.Select(id => new VehicleFeature {FeatureId = id})));
        }
    }
}