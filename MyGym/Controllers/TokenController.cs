using Microsoft.AspNetCore.Mvc;
using MyGym.Database;
using MyGym.Request;
using MyGym.Response;
using MyGym.Service.Contracts;

namespace MyGym.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly MyGymContext _myGymContext;
        private readonly ITokenService _tokenService;

        public TokenController(MyGymContext myGymContext, ITokenService tokenService)
        {
            _myGymContext = myGymContext;
            _tokenService = tokenService;
        }
        [HttpPost]
        [Route("refresh")]
        public ActionResult Refresh(TokenApiModel tokenApiModel) {
            if(tokenApiModel == null) {
                return BadRequest("Invalid client request"); 
            }
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var userName = principal.Identity.Name; //this is mapped to the Name claim by default

            var user = _myGymContext.LoginModels.SingleOrDefault(x => x.UserName == userName);
            if(user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            
            user.RefreshToken = newRefreshToken.RToken;
            _myGymContext.SaveChanges();

            return Ok(new AuthenticatedResponse
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken.RToken
            });
        }
    }
}
