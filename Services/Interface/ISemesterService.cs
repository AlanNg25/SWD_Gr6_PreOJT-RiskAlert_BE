using Repositories.Models;

namespace Services.Interface
{
    public interface ISemesterService
    {
        Task<List<Semester>> GetAllSemestersAsync();
        Task<Semester> GetSemesterByIdAsync(Guid semesterId);
        Task<int> CreateSemesterAsync(Semester semester);
        Task<int> UpdateSemesterAsync(Semester semester);
        Task<int> DeleteSemesterAsync(Guid semesterId);
        Task<List<Semester>> SearchSemesterAsync(string SemesterCode);
    }
}