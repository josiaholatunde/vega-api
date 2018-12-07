using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VegaAPI.Controllers.Resource
{

    public class SaveVehicleResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        public ContactResource Contact{get;set;}
        public DateTime LastUpdated { get; set; }
        public ICollection<int> VehicleFeatures{get;set;}
        public SaveVehicleResource()
        {
            VehicleFeatures = new Collection<int>();
        }
    }
    
}