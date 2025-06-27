using Applications.DTO.Google;
using Azure.Core;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Dtos;
using Services.Implement;
using Services.Interface;

namespace SWD_Gr6_PreOJT_RiskAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceProviders _serviceProviders;

        public AuthController(IServiceProviders serviceProviders)
        {
            _serviceProviders = serviceProviders;
        }

        /// <summary>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var response = await _serviceProviders.AuthService.LoginAsync(loginDto);
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
            await _serviceProviders.AuthService.LogoutAsync(token);
            return NoContent();
        }

        [HttpPost("google")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto dto)
        {
            try
            {
                var res = await _serviceProviders.AuthService.LoginWithGoogleAsync(dto);
                return Ok(res);
            }
            catch (InvalidJwtException)     // từ GoogleJsonWebSignature
            {
                return Unauthorized("Invalid Google token.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error.");
            }
        }

    }
}