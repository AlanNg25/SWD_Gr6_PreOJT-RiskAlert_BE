using Applications.DTO.Response;
using Repositories.Models;

namespace Services.Interface
{
    public interface ISemesterService
    {
        Task<List<SemesterDto>> GetAllSemestersAsync();
        Task<SemesterDto> GetSemesterByIdAsync(Guid semesterId);
        Task<int> CreateSemesterAsync(SemesterDto semester);
        Task<int> UpdateSemesterAsync(SemesterDto semester);
        Task<int> DeleteSemesterAsync(Guid semesterId);
        Task<List<SemesterDto>> SearchSemesterAsync(string SemesterCode);
    }
}