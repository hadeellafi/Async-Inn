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
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _context;

        public RoomsController(IRoom context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [AllowAnonymous]

        [Authorize(Roles = "District Manager")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
          return await _context.GetRooms();
        }

        // GET: api/Rooms/5
        [AllowAnonymous]

        [Authorize(Roles = "District Manager")]
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            return await _context.GetById(id);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "District Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, RoomDTO room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }
            try
            {
                var updateRoom = await _context.Update(id, room);
                return Ok(updateRoom);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "District Manager")]
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(RoomDTO room)
        {
          await _context.Create(room);

            return CreatedAtAction("GetRoom", new { id = room.Id }, room);
        }

        // DELETE: api/Rooms/5
        [Authorize(Roles = "District Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
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

        /* private bool RoomExists(int id)
         {
             return (_context.Rooms?.Any(e => e.Id == id)).GetValueOrDefault();
         }*/


        /////////lab 14
        [Authorize(Roles = "District Manager,Property Manager,Agent")]
        [HttpPost]
        [Route("{roomId}/Amenity/{amenityId}")]
        public async Task<IActionResult> AddAmenityToRoom(int roomId,int amenityId)
        {
            try
            {
                await _context.AddAmenityToRoom(roomId, amenityId);
                return Ok();
            }
            catch (InvalidOperationException ex) { 
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "District Manager,Agent")]
        [HttpDelete]
        [Route("{roomId}/Amenity/{amenityId}")]
        public async Task<IActionResult> RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            try
            {
                await _context.RemoveAmenityFromRoom(roomId, amenityId);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
