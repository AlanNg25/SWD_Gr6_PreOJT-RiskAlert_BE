using Applications.DTO.Create;
using Applications.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceDto>> GetAllAsync();
        Task<AttendanceDto> GetByIdAsync(Guid id);
        Task AddAsync(AttendanceCreateDto attendanceDto);
        Task UpdateAsync(Guid id, AttendanceCreateDto attendanceDto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<AttendanceWithCourseSemesterDto>> GetByUserIdAsync(Guid userId);
    }
}
