using Applications.DTO.Create;
using Applications.DTO.Response;
using Repositories.Models;

namespace Services.Interface
{
    public interface ISemesterService
    {
        Task<List<SemesterDto>> GetAllSemestersAsync();
        Task<SemesterDto> GetSemesterByIdAsync(Guid semesterId);
        Task<Guid> CreateSemesterAsync(SemesterCreateDto semesterDto);
        Task<int> UpdateSemesterAsync(Guid semesterId, SemesterCreateDto semesterDto);
        Task<int> DeleteSemesterAsync(Guid semesterId);
        Task<List<SemesterDto>> SearchSemesterAsync(string SemesterCode);
    }
}