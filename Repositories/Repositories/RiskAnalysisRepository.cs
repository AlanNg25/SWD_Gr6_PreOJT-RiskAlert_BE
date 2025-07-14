using Microsoft.EntityFrameworkCore;
using Repositories.Basic;
using Repositories.DBContext;
using Repositories.Interfaces;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class RiskAnalysisRepository : GenericRepository<RiskAnalysis>, IRiskAnalysisRepository
    {
        public RiskAnalysisRepository(RiskAlertDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RiskAnalysis>> GetAllAsync()
        {
            return await _context.riskAnalysis
                .Where(r => !r.IsDeleted)
                .ToListAsync();
        }

        public async Task<RiskAnalysis> GetByIdAsync(Guid id)
        {
            return await _context.riskAnalysis
                .FirstOrDefaultAsync(r => r.RiskID == id && !r.IsDeleted);
        }

        public async Task AddAsync(RiskAnalysis riskAnalysis)
        {
            riskAnalysis.RiskID = Guid.NewGuid();
            await CreateAsync(riskAnalysis);
        }

        public async Task UpdateAsync(RiskAnalysis riskAnalysis)
        {
            await base.UpdateAsync(riskAnalysis);
        }

        public async Task DeleteAsync(Guid id)
        {
            var riskAnalysis = await GetByIdAsync(id);
            if (riskAnalysis != null)
            {
                riskAnalysis.IsDeleted = true;
                await UpdateAsync(riskAnalysis);
            }
        }
    }
}
