using MyGym.Database;
using MyGym.Database.Entities;
using MyGym.Service.Contracts;
using MyGym.Service.DTOs;

namespace MyGym.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly MyGymContext _context;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserService(
            MyGymContext myGymContext,
            IAuthenticationService authenticationService,
            IUserService userService,
            ITokenService tokenService
            ) {
            _context = myGymContext;
            _authenticationService = authenticationService;
            _userService = userService;
            _tokenService = tokenService;
        }
        public async Task<string> CreateUserAsync(UserDto userDto)
        {
            PasswordDto pwdObj =  _authenticationService.CreatePasswordHash(userDto.Password);

            User user = new User
            {
                UserName = userDto.Username,
                PasswordHash = pwdObj.passwordHash,
                PasswordSalt = pwdObj.passwordSalt,

            };
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return userDto.Username;
        }

    }
}
