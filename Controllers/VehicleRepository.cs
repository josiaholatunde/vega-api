using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VegaAPI.Context;
using VegaAPI.Core;
using VegaAPI.Models;

namespace VegaAPI.Controllers
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaContext _context;

        public VehicleRepository(VegaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Make>> GetMakes()
        {
            var makes = await _context.Makes
                                .Include(m => m.Models).ToListAsync();
            return makes;
        }

        public async Task<IEnumerable<Feature>> GetFeatures()
        {
            var features = await _context.Feature
                                .ToListAsync();
            return features;
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles(Filter filterResource) 
        {
            var query = await _context.Vehicles
                                .Include(v => v.VehicleFeatures)
                                    .ThenInclude(vf => vf.Feature)
                                .Include(v => v.Model)
                                    .ThenInclude(vr => vr.Make)
                                    .ToListAsync();
            if (filterResource.MakeId.HasValue)
               query = query.Where(v => v.Model.MakeId == filterResource.MakeId).ToList();
            
            return query;

        }

        public async Task<Vehicle> GetVehicle(int id, bool includeRelated)
        {
            if (!includeRelated)
               await _context.Vehicles.FindAsync(id);
            var vehicle =  await _context.Vehicles
                                .Include(v => v.VehicleFeatures)
                                    .ThenInclude(vf => vf.Feature)
                                .Include(v => v.Model)
                                    .ThenInclude(vr => vr.Make)
                                .SingleOrDefaultAsync( vf => vf.Id == id);
            return vehicle;
        }
        public void AddVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
        }
        public void RemoveVehicle(Vehicle vehicle)
        {
           _context.Vehicles.Remove(vehicle);
        }

    }
    
}