using MyGym.Service.DTOs;

namespace MyGym.Service.Contracts
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(UserDto user);
    }
}
