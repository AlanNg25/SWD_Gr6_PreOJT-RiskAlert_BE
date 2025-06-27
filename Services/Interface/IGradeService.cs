using Applications.DTO.Response;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IGradeService
    {
        Task<GradeDto> GetByIdAsync(Guid id);
        Task<IEnumerable<GradeDto>> GetAllAsync();
        Task AddAsync(Grade grade);
        Task UpdateAsync(Grade grade);
        Task DeleteAsync(Guid id);
        Task<GradeDetailDto> GetGradeDetailByIdAsync(Guid id);
        Task<IEnumerable<GradeDetailDto>> GetGradeDetailsByGradeIdAsync(Guid gradeId);
        Task AddGradeDetailAsync(GradeDetail gradeDetail);
        Task UpdateGradeDetailAsync(GradeDetail gradeDetail);
        Task DeleteGradeDetailAsync(Guid id);
    }
}
