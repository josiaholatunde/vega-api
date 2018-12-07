using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VegaAPI.Context;
using VegaAPI.Models;
using Microsoft.EntityFrameworkCore;
using VegaAPI.Controllers.Resource;
using AutoMapper;

namespace VegaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VegaController : ControllerBase
    {
        private readonly VegaContext _context;
        private readonly IMapper _mapper;

        public VegaController(VegaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET api/values
        [HttpGet("makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
           var makes = await _context.Makes.Include(m => m.Models).ToListAsync();
            return _mapper.Map<List<Make>, List<MakeResource>>(makes);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
        [HttpGet("vehicles")]
        public async Task<IEnumerable<VehicleResource>> GetVehicles()
        {
           var vehicles = await _context.Vehicles
                                .Include(v => v.VehicleFeatures)
                                    .ThenInclude(vf => vf.Feature)
                                .Include(v => v.Model)
                                .ToListAsync();
            return _mapper.Map<List<Vehicle>, List<VehicleResource>>(vehicles);
        }

        [HttpGet("vehicles/{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var vehicle = await _context.Vehicles
                                    .Include(v => v.VehicleFeatures)
                                        .ThenInclude(vf => vf.Feature)
                                    .Include(v => v.Model)
                                        .ThenInclude(vr => vr.Make)
                                    .SingleOrDefaultAsync( vf => vf.Id == id);
            if (vehicle == null)
                return NotFound("Vehicle Not Found");
            var result =  _mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        // POST api/values
        [HttpPost("vehicles")]
        public  IActionResult Post([FromBody] VehicleResource vehicleResource)
        {
            var res =  _mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            _context.Vehicles.Add(res);
            _context.SaveChanges();
           var result =  _mapper.Map<Vehicle, VehicleResource>(res);
            return Ok(result);
        }

        // PUT api/values/5
        [HttpPut("vehicles/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] VehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var vehicle = await _context.Vehicles.Include(v => v.VehicleFeatures).SingleOrDefaultAsync( vf => vf.Id == id);
            if (vehicle == null)
                return NotFound();
            var res =  _mapper.Map<VehicleResource, Vehicle>(vehicleResource,vehicle);
            await _context.SaveChangesAsync();
            var result =  _mapper.Map<Vehicle, VehicleResource>(res);
            return Ok(result);
        }

        // DELETE api/values/5
        [HttpDelete("vehicles/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
                return NotFound();
            _context.Remove(vehicle);
            await _context.SaveChangesAsync();
            return Ok(id);

        }
    }
}
