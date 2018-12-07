using System.ComponentModel.DataAnnotations.Schema;

namespace VegaAPI.Models
{
    [Table("VehicleFeatures")]
    public class VehicleFeature
    {
        public int FeatureId { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public Feature Feature { get; set; }
    }
    
}