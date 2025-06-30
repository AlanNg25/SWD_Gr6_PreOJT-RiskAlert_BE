using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IGradeRepository
    {
        Task<Grade> GetGradeByIdAsync(Guid id);
        Task<IEnumerable<Grade>> GetAllAsync();
        Task AddAsync(Grade grade);
        Task UpdateAsync(Grade grade);
        Task DeleteAsync(Guid id);
        Task<GradeDetail> GetGradeDetailByIdAsync(Guid id);
        Task<IEnumerable<GradeDetail>> GetGradeDetailsByGradeIdAsync(Guid gradeId);
        Task AddGradeDetailAsync(GradeDetail gradeDetail);
        Task UpdateGradeDetailAsync(GradeDetail gradeDetail);
        Task DeleteGradeDetailAsync(Guid id);
        Task<IEnumerable<Grade>> GetByUserIdAsync(Guid userId);
    }
}
