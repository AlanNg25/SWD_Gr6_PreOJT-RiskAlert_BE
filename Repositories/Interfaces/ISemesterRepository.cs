using Applications.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ISemesterRepository
    {
        Task<List<SemesterDto>> GetAllSemestersAsync();
        Task<SemesterDto> GetSemesterByIdAsync(Guid semesterId);
        Task<int> CreateSemesterAsync(SemesterDto semester);
        Task<int> UpdateSemesterAsync(SemesterDto semester);
        Task<int> DeleteSemesterAsync(Guid semesterId);
        Task<List<SemesterDto>> SearchSemesterAsync(string semesterCode);
    }
}
