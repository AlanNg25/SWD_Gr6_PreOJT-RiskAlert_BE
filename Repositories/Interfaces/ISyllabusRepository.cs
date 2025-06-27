using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ISyllabusRepository
    {
        Task<IEnumerable<Syllabus>> GetAllAsync();
        Task<Syllabus> GetByIdAsync(Guid id);
        Task AddAsync(Syllabus syllabus);
        Task UpdateAsync(Syllabus syllabus);
        Task DeleteAsync(Guid id);
    }
}
