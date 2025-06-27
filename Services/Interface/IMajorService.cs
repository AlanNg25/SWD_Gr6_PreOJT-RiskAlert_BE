using Applications.DTO.Create;
using Applications.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IMajorService
    {
        Task<IEnumerable<MajorDto>> GetAllAsync();
        Task<MajorDto> GetByIdAsync(Guid id);
        Task AddAsync(MajorCreateDto majorDto);
        Task UpdateAsync(Guid id, MajorCreateDto majorDto);
        Task DeleteAsync(Guid id);
    }
}
