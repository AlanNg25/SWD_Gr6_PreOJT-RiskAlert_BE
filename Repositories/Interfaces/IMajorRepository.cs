using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IMajorRepository
    {
        Task<IEnumerable<Major>> GetAllAsync();
        Task<Major> GetByIdAsync(Guid id);
        Task AddAsync(Major major);
        Task UpdateAsync(Major major);
        Task DeleteAsync(Guid id);
    }
}
