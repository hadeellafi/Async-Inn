using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenity _context;

        public AmenitiesController(IAmenity context)
        {
            _context = context;
        }

        // GET: api/Amenities
        [AllowAnonymous]
        [Authorize(Roles = "District Manager , Property Manager", Policy = "Read")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAmenities()
        {
            return await _context.GetAmenities();
        }

        // GET: api/Amenities/5
        [AllowAnonymous]
        [Authorize(Roles = "District Manager , Property Manager", Policy = "Read")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AmenityDTO>> GetAmenity(int id)
        {
         
            return await _context.GetById(id);
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "District Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenity(int id, AmenityDTO amenity)
        {
            if (id != amenity.Id)
            {
                return BadRequest();
            }
            try
            {
                var updateAmenity = await _context.Update(id, amenity);
                return Ok(updateAmenity);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Amenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "District Manager")]
        [HttpPost]
        public async Task<ActionResult<AmenityDTO>> PostAmenity(AmenityDTO amenity)
        {
            await _context.Create(amenity);

            return CreatedAtAction("GetAmenity", new { id = amenity.Id }, amenity);
        }

        // DELETE: api/Amenities/5
        [Authorize(Roles = "District Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            try
            {

                await _context.Delete(id);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*private bool AmenityExists(int id)
        {
            return (_context.Amenities?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
