using Microsoft.AspNetCore.Mvc;
using GameLibraryAPI.Models.DTO.UserDTO;
using GameLibraryAPI.Services;

namespace GameLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _auth.Register(request);
            if (!result.Success)
                return BadRequest(new { error = result.ErrorMessage });

            return StatusCode(201, new { userId = result.User.Id, message = "User registered successfully", token = result.Token, expires = result.ValidTo });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _auth.Login(request);
            if (!result.Success)
                return Unauthorized(new { error = result.ErrorMessage });

            return Ok(new { token = result.Token, refreshToken = result.RefreshToken, expires = result.ValidTo, user = result.User });
        }
    }
}
