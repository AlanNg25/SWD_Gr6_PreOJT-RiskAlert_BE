using Applications.DTO.Google;
using Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Interface
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> LoginWithGoogleAsync(GoogleLoginDto dto);  // Google login
        Task LogoutAsync(string token);
    }
}
