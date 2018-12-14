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
using VegaAPI.Core;

namespace VegaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VegaController : ControllerBase
    {
        private readonly VegaContext _context;
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _repository;

        public VegaController(VegaContext context, IMapper mapper, IVehicleRepository repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
        }
        // GET api/values
        [HttpGet("features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
           var features = await _repository.GetFeatures();
            return _mapper.Map<IEnumerable<Feature>, IEnumerable<KeyValuePairResource>>(features);
        }

         [HttpGet("makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
           var makes = await _repository.GetMakes();
            return _mapper.Map<IEnumerable<Make>, IEnumerable<MakeResource>>(makes);
        }

        [HttpGet("vehicles")]
        public async Task<IEnumerable<VehicleResource>> GetVehicles([FromQuery] FilterResource filterResource)
        {
          var filter =  _mapper.Map<FilterResource, Filter>(filterResource);
           var vehicles = await _repository.GetVehicles(filter);
           return _mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResource>>(vehicles);
        }

        [HttpGet("vehicles/{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var vehicle = await _repository.GetVehicle(id);
            if (vehicle == null)
                return NotFound("Vehicle Not Found");
            var result =  _mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        // POST api/values
        [HttpPost("vehicles")]
        public async Task<IActionResult> Post([FromBody] SaveVehicleResource vehicleResource)
        {
            var res =  _mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            res.LastUpdated = DateTime.Now;
            _repository.AddVehicle(res);
            _context.SaveChanges();
            var vehicle = await _repository.GetVehicle(res.Id);
           var result =  _mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        // PUT api/values/5
        [HttpPut("vehicles/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var vehicle = await _repository.GetVehicle(id);
            if (vehicle == null)
                return NotFound();
            var res =  _mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource,vehicle);
            res.LastUpdated = DateTime.Now;
            await _context.SaveChangesAsync();
            await _repository.GetVehicle(res.Id);
            var result =  _mapper.Map<Vehicle, VehicleResource>(res);
            return Ok(result);
        }

        // DELETE api/values/5
        [HttpDelete("vehicles/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var vehicle = await _repository.GetVehicle(id, false);
            if (vehicle == null)
                return NotFound();
             _repository.RemoveVehicle(vehicle);
            await _context.SaveChangesAsync();
            return Ok(id);

        }
    }
}
