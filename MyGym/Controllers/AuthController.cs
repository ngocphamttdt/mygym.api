using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyGym.Database;
using MyGym.Database.Entities;
using MyGym.Response;
using MyGym.Service.Contracts;
using MyGym.Service.DTOs;
using System.Security.Claims;

namespace MyGym.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MyGymContext _myGymContext;
        private readonly ITokenService _tokenService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        private const string REFRESH_TOKEN = "refresh_token";

        public AuthController(
            MyGymContext myGymContext,
            ITokenService tokenService,
            IAuthenticationService authenticationService,
            IUserService userService
            )
        {
            _myGymContext = myGymContext;
            _tokenService = tokenService;
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] UserDto userModel)
        {
            if (userModel == null)
            {
                return BadRequest("Invalid client request");
            }

            var user = _myGymContext.Users.FirstOrDefault(x => (x.UserName == userModel.Username));

            if (user == null)
            {
                return BadRequest("User not found.");
            }
            else if (!_authenticationService.VerifyPasswordHash(userModel.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userModel.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            SetRefreshTokenCookie(refreshToken);

            user.RefreshToken = refreshToken.RToken;
            user.TokenExpires = refreshToken.Expires;
            _myGymContext.SaveChanges();

            return Ok(new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken.RToken
            });
        }

        [HttpPost, Route("register")]
        public async Task<IActionResult> Register(UserDto user)
        {
            var username = await _userService.CreateUserAsync(user);
            return Ok(new { username });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(LoginModel loginModel)
        {
            var refreshToken = Request.Cookies[REFRESH_TOKEN];
            var user = await _myGymContext.Users.FirstOrDefaultAsync(x => x.UserName == loginModel.UserName);
            if (user == null)
            {
                return Unauthorized("User is not found");
            }
            else if (user.RefreshToken != refreshToken)
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };
            string token = _tokenService.GenerateAccessToken(claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            SetRefreshTokenCookie(newRefreshToken);

            return Ok(new AuthenticatedResponse
            {
                Token = token,
                RefreshToken = newRefreshToken.RToken
            });
        }

        private void SetRefreshTokenCookie(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append(REFRESH_TOKEN, newRefreshToken.RToken);
        }
    }
}
