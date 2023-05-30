using APIExtension.Auth;
using APIExtension.HttpContext;
using AutoMapper;
using DataLayer.DBObject;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using ShareResource.APIModel;
using ShareResource.DTO;
using ShareResource.Enums;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceWrapper services;
        private readonly IMapper mapper;
        public AuthController(IServiceWrapper services, IMapper mapper)
        {
            this.services = services;
            this.mapper = mapper;
        }
        [HttpPost("Register/Student")] 
        public async Task<IActionResult> StudentRegister(AccountRegisterDto dto)
        {
            if(dto.Password!=dto.ConfirmPassword)
            {
                return BadRequest("Xác nhận password không thành công");
            }
            Account register = mapper.Map<Account>(dto);
            await services.Auth.Register(register, RoleNameEnum.Student);
            return Ok(await services.Accounts.GetAccountByUserNameAsync(dto.Username));
        }
        [HttpPost("Register/Parent")]
        public async Task<IActionResult> ParentRegister(AccountRegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
            {
                return BadRequest("Xác nhận password không thành công");
            }
            Account register = mapper.Map<Account>(dto);
            await services.Auth.Register(register, RoleNameEnum.Student);
            return Ok(await services.Accounts.GetAccountByUserNameAsync(dto.Username));
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginModel loginModel)
        {
            Account logined = await services.Auth.LoginAsync(loginModel);
            if (logined is null)
            {
                return Unauthorized("Username or password is wrong");
            }
            return Ok(await services.Auth.GenerateJwtAsync(logined, loginModel.RememberMe));
        }
        [CustomGoogleIdTokenAuthFilter]
        [HttpPost("Login/Google/Id-Token")]
        public async Task<IActionResult> LoginWithGoogleIdTokenAsync(bool rememberMe=true)
        {
            //var idToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = HttpContext.GetGoogleIdToken();
            Account logined = await services.Auth.LoginWithGoogle(idToken);
            if (logined is null)
            {
                return Unauthorized("Username or password is wrong");
            }
            return Ok(await services.Auth.GenerateJwtAsync(logined, rememberMe));
        }
        [HttpGet("TestAuth/Tokens")]
        public async Task<IActionResult> GetToken()
        {
            var accerssToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");
            var headerToken = HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value;
            return Ok(new
            {
                Access_Token = accerssToken,
                Id_Token = idToken,
                Header_Token = headerToken,
            });
        }
        [Authorize]
        [HttpGet("TestAuth/LoginData")]
        public async Task<IActionResult> GetRoleData()
        {
            string? roles1 = String.Empty;
            foreach (var roleClaim in User.Claims.Where(role => role.Type.Contains("role", StringComparison.CurrentCultureIgnoreCase)))
            {
                roles1 += $" {roleClaim.Value}";
            }
            string emails = String.Empty;
            IEnumerable<Claim> claims = User.Claims;
            IEnumerable<Claim> emailClaims = claims.Where<Claim>(role => role.Type.Contains("mail", StringComparison.CurrentCultureIgnoreCase));
            foreach (var emailClaim in emailClaims)
            {
                emails += $" {emailClaim.Value}";
            }
            return Ok(new
            {
                roles = string.IsNullOrEmpty(roles1) ? "No role" : roles1,
                emails = string.IsNullOrEmpty(emails) ? "No mail" : emails
            });
        }
        [Authorize(Roles = "Student")]
        [HttpGet("TestAuth/Student")]
        public async Task<IActionResult> GetStudentData()
        {
            return Ok("You're student ");
        }

        [Authorize(Roles = "Parent")]
        [HttpGet("TestAuth/Parent")]
        public async Task<IActionResult> GetUserData()
        {
            return Ok("You're a parent ");
        }
    }
}
