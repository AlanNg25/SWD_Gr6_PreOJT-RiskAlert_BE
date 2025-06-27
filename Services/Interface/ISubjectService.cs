using Applications.DTO.Create;
using Applications.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetAllAsync();
        Task<SubjectDto> GetByIdAsync(Guid id);
        Task AddAsync(SubjectCreateDto subjectDto);
        Task UpdateAsync(Guid id, SubjectCreateDto subjectDto);
        Task DeleteAsync(Guid id);
    }
}
