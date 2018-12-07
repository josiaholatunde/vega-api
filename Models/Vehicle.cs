using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace VegaAPI.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Model Model { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime LastUpdated { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public int ContactPhone { get; set; }
        public ICollection<VehicleFeature> VehicleFeatures{get;set;}
        public Vehicle()
        {
            VehicleFeatures = new Collection<VehicleFeature>();
            
        }
    }
    
}