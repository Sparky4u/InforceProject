using InforceProject.Server.Models.LoginModel;
using InforceProject.Server.Models.RegisterModel;
using InforceProject.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace InforceProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid login data" });

            var user = await _authService.FindUserByNameAsync(model.UserName);

            if (user == null || !await _authService.ValidatePasswordAsync(user, model.Password))
                return Unauthorized(new { message = "Invalid login attempt" });

            var token = _authService.GenerateJwtToken(user);
            return Ok(new
            {
                token,
                message = "Login successful",
                userName = user.UserName,
                role = user.Role
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid registration data" });

            var result = await _authService.CreateUserAsync(model.UserName, model.Password, model.Role);

            if (result.Succeeded)
            {
                return Ok(new { message = "Register successful" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("logout")]
        public IActionResult Logout() => Ok(new { message = "Logout successful" });
    }
}
