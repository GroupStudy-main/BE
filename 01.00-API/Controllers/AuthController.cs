using DataLayer.DBObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using ShareResource.APIModel;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceWrapper services;
        public AuthController(IServiceWrapper sesrvices)
        {
            this.services = services;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginModel loginModel)
        {
               Account logined = await services.Auth.LoginAsync(loginModel);
            return Ok(services.Auth.GenerateJwt(logined, loginModel.RememberMe));
        }
        [HttpPost("Login/Google")]
        public async Task<IActionResult> LoginWithGoogleAsync(bool rememberMe)
        {
            Account logined = await services.Auth.LoginWithGoogle("");
            return Ok(services.Auth.GenerateJwt(logined, rememberMe));
        }
    }
}
