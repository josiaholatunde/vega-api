using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            var query =  _context.Vehicles
                                .Include(v => v.VehicleFeatures)
                                    .ThenInclude(vf => vf.Feature)
                                .Include(v => v.Model)
                                    .ThenInclude(vr => vr.Make)
                                    .AsQueryable();
            if (filterResource.MakeId.HasValue)
               query = query.Where(v => v.Model.MakeId == filterResource.MakeId).AsQueryable();
            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>(){
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
                ["id"] = v => Convert.ToString(v.Id),
            }; 
 
            query = ApplyOrdering(filterResource,query,columnsMap);
            return await query.ToListAsync();

        }
        private IQueryable<Vehicle> ApplyOrdering(Filter filterResource, IQueryable<Vehicle> query, Dictionary<string, Expression<Func<Vehicle, object>>> columnsMap )
        {
            if (String.IsNullOrWhiteSpace(filterResource.SortBy) || !columnsMap.ContainsKey(filterResource.SortBy)) {
                return query;
            }
            if(filterResource.IsAscending) 
            {
                return query.OrderBy(columnsMap[filterResource.SortBy]);
            } 
            else 
            {
                return query = query.OrderByDescending(columnsMap[filterResource.SortBy]);
            }
            
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