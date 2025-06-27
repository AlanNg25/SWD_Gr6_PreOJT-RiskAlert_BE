using Applications.DTO.Create;
using Applications.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ICurriculumService
    {
        Task<IEnumerable<CurriculumDto>> GetAllAsync();
        Task<CurriculumDto> GetByIdAsync(Guid id);
        Task AddAsync(CurriculumCreateDto curriculumDto);
        Task UpdateAsync(Guid id, CurriculumCreateDto curriculumDto);
        Task DeleteAsync(Guid id);
    }
}
