using ApplicationLayer.DTO;
using ApplicationLayer.Interface;
using DomainLayer;
using InfrastructureLayer.Interface;

using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IRegister _register;
        private readonly ILogin _login;
        private readonly IForgotPassword _forgotpassword; 

        public UserController(IRegister register,ILogin login,IForgotPassword forgotPassword)
        {
            _register = register;
            _login = login;
           _forgotpassword = forgotPassword;
        }
       
            [HttpPost]
        [Route("Register")]
        public IActionResult Add(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("not a valid request");
            }
            var temp = _register.Add(userRegisterDto);
            if ( temp== "OK")
            {
                return Ok(temp);
            }
            else
            {
                return BadRequest(temp);
            }
        }
        [HttpGet]
        [Route("Get")]
        public ActionResult<List<UserRegister>> Get()
        {
            return _register.Get();
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            var result = _login.Login(userLoginDto);
            if (result == "user not found")
            {
                return BadRequest("user not found");
            }
            else if (result == "wrong password")
            {
                return BadRequest("wrong password");
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult Forgot(ForgotPasswordDto forgotPasswordDto )
        {
            var temp=_forgotpassword.ForgotPassword(forgotPasswordDto);
            if (temp== "Email sent")
            { 
                return Ok(temp); 
            }
            else
            {
                return BadRequest(temp);
            }
              
        }

    }
}
