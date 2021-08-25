using AspNet_Core_API__JWT_Authentication_.JwtService;
using AspNet_Core_API__JWT_Authentication_.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNet_Core_API__JWT_Authentication_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtHandler jwtHandler;
        public AccountController(IJwtHandler _jwtHandler)
        {
            jwtHandler = _jwtHandler;
        }


        [HttpPost("register")]
        public IActionResult Register(Register register)
        {
            if (ModelState.IsValid)
            {

                Response.Cookies.Append("jwt", jwtHandler.GenerateJWTToken(register.UserName));
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                //Get user from database validate the credentials 
                // if successfull generate token and append it to response
                
                Response.Cookies.Append("jwt", jwtHandler.GenerateJWTToken(login.UserName));
                return Ok();

            }

            return BadRequest();
        }
    }
}
