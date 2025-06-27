using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ISuggestionRepository
    {
        Task<IEnumerable<Suggestion>> GetAllAsync();
        Task<Suggestion> GetByIdAsync(Guid id);
        Task AddAsync(Suggestion suggestion);
        Task UpdateAsync(Suggestion suggestion);
        Task DeleteAsync(Guid id);
    }
}
