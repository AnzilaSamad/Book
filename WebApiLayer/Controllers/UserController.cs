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
        public UserController(IRegister register)
        {
            _register = register;
        }
       
            [HttpPost]
        [Route("Register")]
        public IActionResult Add(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("not a valid request");
            }
            _register.Add(userRegisterDto);
            return Ok();
        }
        [HttpGet]
        [Route("Get")]
        public ActionResult<List<UserRegister>> Get()
        {
            return _register.Get();
        }

        
    }
}
