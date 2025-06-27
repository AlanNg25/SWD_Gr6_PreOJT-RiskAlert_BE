using Applications.DTO.Create;
using Applications.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ISyllabusService
    {
        Task<IEnumerable<SyllabusDto>> GetAllAsync();
        Task<SyllabusDto> GetByIdAsync(Guid id);
        Task AddAsync(SyllabusCreateDto syllabusDto);
        Task UpdateAsync(Guid id, SyllabusCreateDto syllabusDto);
        Task DeleteAsync(Guid id);
    }
}
