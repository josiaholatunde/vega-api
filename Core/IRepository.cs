using System.Collections.Generic;
using System.Threading.Tasks;
using VegaAPI.Models;

namespace VegaAPI.Core
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Feature>> GetFeatures();
        Task<IEnumerable<Make>> GetMakes();
        Task<IEnumerable<Vehicle>> GetVehicles(Filter filterResource);
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        void AddVehicle(Vehicle vehicle);
        void RemoveVehicle(Vehicle vehicle);
    }

}