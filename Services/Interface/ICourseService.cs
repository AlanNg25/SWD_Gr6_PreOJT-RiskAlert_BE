using Applications.DTO.Create;
using Applications.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllAsync();
        Task<CourseDto> GetByIdAsync(Guid id);
        Task AddAsync(CourseCreateDto courseDto);
        Task UpdateAsync(Guid id, CourseCreateDto courseDto);
        Task DeleteAsync(Guid id);
    }
}
