using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using vega.Controllers.Resources;
using vega.Domain.Models;
using vega.Domain.Models.Features;
using vega.Domain.Models.Vehicle;

namespace vega.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API
            CreateMap<Photo, PhotoResource>();
            CreateMap<VehicleMake, VehicleMakeResource>();
            CreateMap<VehicleMake, BaseResource>();
            CreateMap<VehicleModel, VehicleModelResource>();
            CreateMap<Feature, FeatureResource>();
            
            CreateMap<Vehicle, SaveVehicleResource>()
             .ForMember(v => v.Contact, opt => opt.MapFrom(resource => new ContactResource { 
                 Name = resource.ContactName, 
                 Email = resource.ContactEmail,
                 Phone = resource.ContactPhone 
            }))
            .ForMember(v => v.Features, opt => opt.MapFrom(resource => resource.Features.Select(i => i.FeatureId)));
            
            CreateMap<Vehicle, VehicleResource>()
            .ForMember(v => v.Make, opt => opt.MapFrom(resource => resource.VehicleModel.Make))
            .ForMember(v => v.Contact, opt => opt.MapFrom(resource => new ContactResource { 
                 Name = resource.ContactName, 
                 Email = resource.ContactEmail,
                 Phone = resource.ContactPhone 
            }))
            .ForMember(v => v.Features, 
             opt => opt.MapFrom(resource => resource.Features.Select(
                 i => new FeatureResource {Id = i.Feature.Id, Name = i.Feature.Name}
                )));

            // API to Domain
            CreateMap<VehicleQueryResource, VehicleQuery>();
            CreateMap<SaveVehicleResource, Vehicle>()
            .ForMember(v => v.Id, opt => opt.Ignore())
            .ForMember(v => v.ContactName, opt => opt.MapFrom(resource => resource.Contact.Name))
            .ForMember(v => v.ContactEmail, opt => opt.MapFrom(resource => resource.Contact.Email))
            .ForMember(v => v.ContactPhone, opt => opt.MapFrom(resource => resource.Contact.Phone))
            .ForMember(v => v.Features, opt => opt.Ignore())
            .AfterMap((vResource, v) => {
                var featuresToRemove = v.Features.Where(i => !vResource.Features.Contains(i.FeatureId)); 
                foreach(var f in featuresToRemove)
                    v.Features.Remove(f);

                var featuresToAdd = vResource.Features.Where(i => !v.Features.Any(f => f.FeatureId == i)); 
                foreach(var id in featuresToAdd)
                    v.Features.Add(new VehicleFeature {FeatureId = id});
            });
        }
    }
}