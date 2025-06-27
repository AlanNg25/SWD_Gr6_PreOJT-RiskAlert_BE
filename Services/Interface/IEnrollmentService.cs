using Applications.DTO.Create;
using Applications.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDto>> GetAllAsync();
        Task<EnrollmentDto> GetByIdAsync(Guid id);
        Task AddAsync(EnrollmentCreateDto enrollmentDto);
        Task UpdateAsync(Guid id, EnrollmentCreateDto enrollmentDto);
        Task DeleteAsync(Guid id);
    }
}
