using Async_Inn.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Async_Inn.Models.Interfaces
{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterUserDTO registerUser, ModelStateDictionary modelState);
        public Task<UserDTO> Authenticate(string username, string password);

    }
}
