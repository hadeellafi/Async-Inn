using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            string connString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services
                .AddDbContext<AsyncInnDbContext>(options => options.UseSqlServer(connString));

            ////lab13 repository design pattern
            builder.Services.AddTransient<IHotel, HotelService>();

            builder.Services.AddTransient<IRoom, RoomService>();

            builder.Services.AddTransient<IAmenity, AmenityService>();


            ///////
            var app = builder.Build();

            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.MapGet("/hey", () => "hey");
            app.Run();
        }
    }
}