﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;

namespace Async_Inn.Controllers
{
    [Route("api/Hotels/{hotelId}/Rooms")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _context;

        public HotelRoomsController(IHotelRoom context)
        {
            _context = context;
        }

        // GET: /api/Hotels/{hotelId}/Rooms
        //get all rooms for this hotel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms(int hotelId)
        {
            return Ok(await _context.Get( hotelId));
        }

        //GET all room details for a specific room
        //    /api/Hotels/{hotelId}/Rooms/{roomNumber}
        [HttpGet("{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int hotelId,int roomNumber)
        {

            try
            {
                var hotelRoom= await _context.GetById(hotelId, roomNumber);
                return Ok(hotelRoom);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }



        }

        // PUT: /api/Hotels/{hotelId}/Rooms/{roomNumber}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int roomNumber, HotelRoom updatedHotelRoom)
        {
            try
            {
                var hotelRoom = await _context.Update(hotelId, roomNumber, updatedHotelRoom);
                return Ok(hotelRoom); // Return 200 OK status along with the updated hotel room
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //POST to add a room to a hotel: /api/Hotels/{hotelId}/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(int hotelId,HotelRoom hotelRoom)
        {
            try
            {
                var roomToAdd = await _context.Create(hotelRoom, hotelId);

                // Rurtn a 201 Header to Browser or the postmane
                return Ok(roomToAdd);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

        //DELETE a specific room from a hotel:
        //api/Hotels/{hotelId}/Rooms/{roomNumber}
        [HttpDelete("{roomNumber}")]
        public async Task<IActionResult> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            try
            {
                await _context.Delete(hotelId, roomNumber);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

       /* private bool HotelRoomExists(int id)
        {
            return (_context.HotelRooms?.Any(e => e.HotelId == id)).GetValueOrDefault();
        }*/
    }
}