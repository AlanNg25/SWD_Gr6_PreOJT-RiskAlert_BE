using Applications.DTO.Create;
using Applications.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(Guid id);
        Task AddAsync(UserCreateDto userDto);
        Task UpdateAsync(Guid id, UserCreateDto userDto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<UserDto>> GetByRoleAsync(string role);
    }
}
