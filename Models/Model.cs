using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace VegaAPI.Models
{
    public class Model
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public Make Make { get; set; }
        public int MakeId { get; set; }
        public ICollection<Vehicle> Vehicles{get;set;}
        public Model()
        {
            Vehicles = new Collection<Vehicle>();
        }
    }
    
}