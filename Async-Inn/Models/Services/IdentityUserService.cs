using Async_Inn.Models.DTO;
using Async_Inn.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Async_Inn.Models.Services
{
    public class IdentityUserService : IUser
    {
        private UserManager<ApplicationUser> userManager;

        public IdentityUserService(UserManager<ApplicationUser> manager)
        {
            userManager = manager;
        }

        public async Task<UserDTO> Authenticate(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);
            bool vaildPassword = await userManager.CheckPasswordAsync(user, password);
            if (vaildPassword)
            {
                return new UserDTO { Id = user.Id, Username = user.UserName };

            }
            return null;
        }

        public async Task<UserDTO> Register(RegisterUserDTO registerUser, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser
            {
                UserName = registerUser.Username,
                Email = registerUser.Email,
                PhoneNumber = registerUser.PhoneNumber,
            };
            var result = await userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.UserName
                };
            }
            foreach (var error in result.Errors)
            {
                var errorKey = error.Code.Contains("Password") ? nameof(registerUser.Password) :
                             error.Code.Contains("Email") ? nameof(registerUser.Email) :
                             error.Code.Contains("Username") ? nameof(registerUser.Username) :
                             "";

                modelState.AddModelError(errorKey, error.Description);
            }
            return null;
        }
    }
}
