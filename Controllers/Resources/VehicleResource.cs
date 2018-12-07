using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VegaAPI.Controllers.Resource
{

    public class VehicleResource
    {
        public int Id { get; set; }
        public KeyValuePairResource Model { get; set; }
        public MakeResource Make {get;set;}
        public bool IsRegistered { get; set; }
        public DateTime LastUpdated { get; set; }
        public ContactResource Contact { get; set; }
        public ICollection<KeyValuePairResource> VehicleFeatures{get;set;}
        public VehicleResource()
        {
            VehicleFeatures = new Collection<KeyValuePairResource>();
            
        }  

    }

}