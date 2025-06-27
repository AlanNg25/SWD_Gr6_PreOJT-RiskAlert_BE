using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICurriculumRepository
    {
        Task<IEnumerable<Curriculum>> GetAllAsync();
        Task<Curriculum> GetByIdAsync(Guid id);
        Task AddAsync(Curriculum curriculum);
        Task UpdateAsync(Curriculum curriculum);
        Task DeleteAsync(Guid id);
    }
}
