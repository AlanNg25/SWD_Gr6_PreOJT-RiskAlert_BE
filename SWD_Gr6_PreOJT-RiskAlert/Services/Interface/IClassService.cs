using Repositories.Models;

namespace Services.Interface
{
    public interface IClassService
    {
        Task<List<Classes>> GetAllClassesAsync();
        Task<Classes> GetClassByIdAsync(Guid classId);
        Task<int> CreateClassAsync(Classes classes);
        Task<int> UpdateClassAsync(Classes classes);
        Task<int> DeleteClassAsync(Guid classId);
        Task<List<Classes>> SearchClassAsync(string classCode, string className);
    }
}