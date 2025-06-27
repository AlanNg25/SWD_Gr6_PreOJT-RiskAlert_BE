using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IRiskAnalysisRepository
    {
        Task<IEnumerable<RiskAnalysis>> GetAllAsync();
        Task<RiskAnalysis> GetByIdAsync(Guid id);
        Task AddAsync(RiskAnalysis riskAnalysis);
        Task UpdateAsync(RiskAnalysis riskAnalysis);
        Task DeleteAsync(Guid id);
    }
}
