using Applications.DTO.Response;
using Repositories.Models;
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
        Task<Guid> CreateSemesterAsync(Semester semester);
        Task<int> UpdateSemesterAsync(Semester semester);
        Task<int> DeleteSemesterAsync(Guid semesterId);
        Task<List<SemesterDto>> SearchSemesterAsync(string semesterCode);
        Task<Semester> GetSemesterByIdRawAsync(Guid semesterId);
    }
}
