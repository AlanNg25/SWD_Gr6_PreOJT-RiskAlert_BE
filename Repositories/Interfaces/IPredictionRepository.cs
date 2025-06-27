using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IPredictionRepository
    {
        Task<IEnumerable<Prediction>> GetAllAsync();
        Task<Prediction> GetByIdAsync(Guid id);
        Task AddAsync(Prediction prediction);
        Task UpdateAsync(Prediction prediction);
        Task DeleteAsync(Guid id);
    }
}
