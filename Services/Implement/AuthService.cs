using Applications.DTO.Google;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using Repositories.Dtos;
using Repositories.Interfaces;
using Repositories.Models;
using Repositories.Repositories;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Services.Implement
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly HashSet<string> _blacklistedTokens = new HashSet<string>();

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var token = GenerateJwtToken(user);
            return new AuthResponseDto
            {
                Token = token,
                FullName = user.FullName,
                Role = user.Role
            };
        }

        public async Task<AuthResponseDto> LoginWithGoogleAsync(GoogleLoginDto dto)
        {
            // 1. Xác thực ID token (chữ ký, audience, exp…)
            var payload = await GoogleJsonWebSignature.ValidateAsync(
                dto.IdToken,
                new ValidationSettings { Audience = new[] { _configuration["Google:ClientId"] } });

            // 2. Lấy email
            var email = payload.Email;
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(email);

            // 2a. Nếu user CHƯA tồn tại → tạo “tài khoản Google” mặc định
            if (user == null)
            {
                user = new User
                {
                    UserID = Guid.NewGuid(),
                    Email = email,
                    FullName = payload.Name ?? email,
                    Role = "Student",          // gán role mặc định
                    Status = 1,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    Password = ""                 // để trống – chỉ login qua Google
                };
                await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.SaveChangesWithTransactionAsync();
            }

            // 3. Tạo JWT nội bộ
            return new AuthResponseDto
            {
                Token = GenerateJwtToken(user),
                FullName = user.FullName,
                Role = user.Role
            };
        }

        public Task LogoutAsync(string token)
        {
            _blacklistedTokens.Add(token);
            return Task.CompletedTask;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserID.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
