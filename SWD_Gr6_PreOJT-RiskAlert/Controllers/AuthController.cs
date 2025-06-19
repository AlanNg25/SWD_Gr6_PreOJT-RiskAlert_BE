using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Dtos;
using Services.Interface;

namespace SWD_Gr6_PreOJT_RiskAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Logs in a user and returns a JWT token.
        /// </summary>
        /// <param name="loginDto">User credentials</param>
        /// <returns>JWT token and user info</returns>
        /// <response code="200">Returns the JWT token and user info</response>
        /// <response code="401">Invalid email or password</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var response = await _authService.LoginAsync(loginDto);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid email or password.");
            }
        }

        /// <summary>
        /// Logs out a user by invalidating the JWT token.
        /// </summary>
        /// <param name="token">JWT token to invalidate</param>
        /// <returns>No content</returns>
        /// <response code="204">Successfully logged out</response>
        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            await _authService.LogoutAsync(token);
            return NoContent();
        }
    }
}