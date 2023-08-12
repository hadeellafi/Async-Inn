using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Async_Inn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            //to print use include list & after install package lab 14
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
     );
            /////////////////////////////////

            string connString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services
                .AddDbContext<AsyncInnDbContext>(options => options.UseSqlServer(connString));

            /* lab 18 identity */
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                // There are other options like this
            }).AddEntityFrameworkStores<AsyncInnDbContext>();

            builder.Services.AddTransient<IUser, IdentityUserService>();

            ////////////lab18 identity


            ////lab13 repository design pattern
            builder.Services.AddTransient<IHotel, HotelService>();

            builder.Services.AddTransient<IRoom, RoomService>();

            builder.Services.AddTransient<IAmenity, AmenityService>();

            builder.Services.AddTransient<IHotelRoom, HotelRoomRepository>();
        
            //lab 19 roles 
            builder.Services.AddScoped<JwtTokenService>();


            //add authentication method
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // Tell the authenticaion scheme "how/where" to validate the token + secret
                options.TokenValidationParameters = JwtTokenService.GetValidateParameters(builder.Configuration);
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Create", policy => policy.RequireClaim("permissions", "Create"));
                options.AddPolicy("Read", policy => policy.RequireClaim("permissions", "Read"));
                options.AddPolicy("Update", policy => policy.RequireClaim("permissions", "Update"));
                options.AddPolicy("Delete", policy => policy.RequireClaim("permissions", "Delete"));

            });

            /////lab 17 add swagger
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Async-Inn API",
                    Version = "v1",
                });
            });

            ///////
            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

          

            ///lab17
            app.UseSwagger(aptions =>
            {
                aptions.RouteTemplate = "/api/{documentName}/swagger.json";
            });
            //to add some style 
            app.UseSwaggerUI(aptions =>
            {
                aptions.SwaggerEndpoint("/api/v1/swagger.json", "Async-Inn API");
                aptions.RoutePrefix = "docs";
            });
            ///

            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.MapGet("/hey", () => "hey");
            app.Run();
        }
    }
}