using Async_Inn.Models;
using Async_Inn.Models.DTO;
using Async_Inn.Models.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.Intrinsics.X86;

namespace TestAsync
{
    public class UnitTest1 : Mock
    {
        [Fact]
        public async Task CanGetRoomById()
        {
            // Arrange
            var room = await CreateAndSaveTestRoom();

            var service = new RoomService(_db);

            // Act
            var retrievedRoom = await service.GetById(room.Id);

            // Assert
            Assert.NotNull(retrievedRoom);
            Assert.Equal(room.Name, retrievedRoom.Name);
            Assert.Equal(room.RoomLayout, retrievedRoom.RoomLayout);
        }
        [Fact]
        public async Task CanUpdateRoom()
        {
            // Arrange 
            var room = await CreateAndSaveTestRoom();
            var service = new RoomService(_db);

            var updatedRoomDTO = new RoomDTO
            {
                Id = room.Id,
                Name = "Updated Room Name",
                RoomLayout = Layout.OneBedroom
            };

            // Act
            var updatedRoom = await service.Update(room.Id, updatedRoomDTO);

            // Assert
            Assert.NotNull(updatedRoom);
            Assert.Equal(updatedRoomDTO.Name, updatedRoom.Name);

            // Check that the room is updated in the database
            var updatedRoomFromDb = await _db.Rooms.FindAsync(room.Id);
            Assert.NotNull(updatedRoomFromDb);
            Assert.Equal(updatedRoomDTO.Name, updatedRoomFromDb.Name);
        }
        [Fact]
        public async void CanAddAminentyToRoom()
        {
            // Arrange 
            var room = await CreateAndSaveTestRoom();
            var amenity = await CreateAndSaveTestAmenity();

            var service = new RoomService(_db);


            // Act
            await service.AddAmenityToRoom(room.Id, amenity.Id);

            // Assert
            RoomDTO updatedRoom = await service.GetById(room.Id);


            Assert.Contains(updatedRoom.Amenities, a => a.Id == amenity.Id);


        }
        [Fact]
        public async Task CanRemoveAmenityFromRoom()
        {
            // Arrange
            var room = await CreateAndSaveTestRoom();
            var amenity = await CreateAndSaveTestAmenity();
            var service = new RoomService(_db);

            // Add the amenity 
            await service.AddAmenityToRoom(room.Id, amenity.Id);

            // Act
            await service.RemoveAmenityFromRoom(room.Id, amenity.Id);

            // Assert
            var updatedRoom = await service.GetById(room.Id);
            Assert.DoesNotContain(updatedRoom.Amenities, a => a.Id == amenity.Id);
        }

        //////////hotel room tests 

        [Fact]
        public async void CanCreateHotelRoom()
        {
            var hotel = await CreateAndSaveTestHotel();
            var room = await CreateAndSaveTestRoom();

            var hotelRoomDTO = new HotelRoomDTO
            {
                HotelId = hotel.Id,
                RoomId = room.Id,
                RoomNumber = 1,
                IsPetFriendly = true,
                Rate = 5
            };

            var service = new HotelRoomRepository(_db);

            var createdHotelRoom = await service.Create(hotelRoomDTO, hotel.Id);

            Assert.NotNull(createdHotelRoom);
            Assert.Equal(hotelRoomDTO.HotelId, createdHotelRoom.HotelId);
            Assert.Equal(hotelRoomDTO.RoomId, createdHotelRoom.RoomId);
            Assert.Equal(hotelRoomDTO.RoomNumber, createdHotelRoom.RoomNumber);
            Assert.Equal(hotelRoomDTO.IsPetFriendly, createdHotelRoom.IsPetFriendly);
            Assert.Equal(hotelRoomDTO.Rate, createdHotelRoom.Rate);
        }
        [Fact]
        public async void CanDeleteHotelRoom()
        {
            var hotel = await CreateAndSaveTestHotel();
            var room = await CreateAndSaveTestRoom();
            var hotelRoomDTO = new HotelRoomDTO
            {
                HotelId = hotel.Id,
                RoomId = room.Id,
                RoomNumber = 1,
                IsPetFriendly = true,
                Rate = 4
            };

            var service = new HotelRoomRepository(_db);

            await service.Create(hotelRoomDTO, hotel.Id);

            await service.Delete(hotel.Id, hotelRoomDTO.RoomNumber);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.GetById(hotel.Id, hotelRoomDTO.RoomNumber));
        }

        [Fact]
        public async void CanUpdateHotelRoom()
        {
            // Arrange
            var hotel = await CreateAndSaveTestHotel();
            var room = await CreateAndSaveTestRoom();
            var hotelRoomDTO = new HotelRoomDTO
            {
                HotelId = hotel.Id,
                RoomId = room.Id,
                RoomNumber = 101,
                IsPetFriendly = true,
                Rate = 3
            };

            var service = new HotelRoomRepository(_db);

            // Create the hotel room
            await service.Create(hotelRoomDTO, hotel.Id);

            // Modify some properties of the hotel room DTO
            hotelRoomDTO.IsPetFriendly = false;
            hotelRoomDTO.Rate =5 ;

            // Act
            await service.Update(hotel.Id, hotelRoomDTO.RoomNumber, hotelRoomDTO);

            // Assert
            var retrievedHotelRoom = await service.GetById(hotel.Id, hotelRoomDTO.RoomNumber);
            Assert.Equal(hotelRoomDTO.IsPetFriendly, retrievedHotelRoom.IsPetFriendly);
            Assert.Equal(hotelRoomDTO.Rate, retrievedHotelRoom.Rate);
        }

    }






}



    
