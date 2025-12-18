using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Collaborative_Tasks.Services;
using Collaborative_Tasks.DTOs;

namespace Collaborative_Tasks.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly FirebaseAuthService _firebaseAuth;
        private readonly JwtService _jwtService;

        public AuthController(
            FirebaseAuthService firebaseAuth,
            JwtService jwtService)
        {
            _firebaseAuth = firebaseAuth;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = await _firebaseAuth.RegisterAsync(
                dto.Email,
                dto.Password
            );

            var token = _jwtService.GenerateToken(
                user.Uid,
                user.Email
            );

            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _firebaseAuth.LoginAsync(
                dto.Email,
                dto.Password
            );

            var token = _jwtService.GenerateToken(
                user.Uid,
                user.Email
            );

            return Ok(new { token });
        }
    }
}
