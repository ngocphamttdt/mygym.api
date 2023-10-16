using Azure.Core;
using Microsoft.AspNetCore.Http;
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

        private const string REFRESH_TOKEN = "refresh_token";

        public AuthController(MyGymContext myGymContext, ITokenService tokenService)
        {
            _myGymContext = myGymContext;
            _tokenService = tokenService;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Invalid client request");
            }
            var user = _myGymContext.LoginModels.FirstOrDefault(x => (x.UserName == loginModel.UserName) && (x.Password == loginModel.Password));

            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            SetRefreshTokenCookie(refreshToken);

            user.RefreshToken = refreshToken.RToken;
            user.RefreshTokenExpiryTime = refreshToken.Expires;
            _myGymContext.SaveChanges();

            return Ok(new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken.RToken
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(LoginModel loginModel)
        {
            var refreshToken = Request.Cookies[REFRESH_TOKEN];
            var user = await _myGymContext.LoginModels.FirstOrDefaultAsync(x => x.UserName == loginModel.UserName);
            if (user == null)
            {
                return Unauthorized("User is not found");
            }
            else if(user.RefreshToken != refreshToken) {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if(user.RefreshTokenExpiryTime <DateTime.Now)
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
