
using System.Linq;
using VegaAPI.Controllers.Resource;
using VegaAPI.Models;

namespace VegaAPI.Profile
{
    public class MappingProfile: AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
            .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource{ Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone}))
            .ForMember( vr => vr.VehicleFeatures, opt => opt.MapFrom( v => v.VehicleFeatures.Select(vf => vf.FeatureId)));
             CreateMap<Vehicle, VehicleResource>()
             .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
            .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource{ Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone}))
            .ForMember( vr => vr.VehicleFeatures, opt => opt.MapFrom( v => v.VehicleFeatures.Select(vf => new KeyValuePairResource{ Id = vf.Feature.Id, Name = vf.Feature.Name})));

            // Map API Resource to Domain Resource
            CreateMap<SaveVehicleResource, Vehicle>()
            .ForMember(v => v.Id, opt => opt.Ignore())
            .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
            .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
            .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
            .ForMember(v => v.VehicleFeatures, opt => opt.Ignore())
            .AfterMap((vr, v) => {
               var removedFeatures = v.VehicleFeatures.Where(vf => !vr.VehicleFeatures.Contains(vf.FeatureId));
                foreach (var feature in removedFeatures)
                    v.VehicleFeatures.Remove(feature);
                var addedFeature = vr.VehicleFeatures.Where(id => !v.VehicleFeatures.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature{ FeatureId = id});
                foreach (var feature in addedFeature)
                    v.VehicleFeatures.Add(feature);
         
            });
            
        }
        
    }
    
}