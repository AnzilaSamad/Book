using DomainLayer;
using InfrastructureLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {

        private readonly AppDbContext _dataContext;
        private readonly IConfiguration _config;
        public LoginApiController(AppDbContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _config = configuration;

        }
        public static UserVerify verify = new UserVerify();
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserLogin request)
        {

            if (_dataContext.VerifyDetails.Any(u => u.UserName == request.Username))
            {
                return BadRequest("User already exist");
            }
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new UserVerify
            {
                UserName = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            _dataContext.VerifyDetails.Add(user);
            await _dataContext.SaveChangesAsync();
            return Ok("user Succesfully created");





//        [HttpPost]
//        public async Task<IActionResult>Login(UserRegister model)
//        {
//            if(Model)
//    }
//}
