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
    public class PredictionRepository : GenericRepository<Prediction>, IPredictionRepository
    {
        public PredictionRepository(RiskAlertDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Prediction>> GetAllAsync()
        {
            return await _context.prediction
            .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<Prediction> GetByIdAsync(Guid id)
        {
            return await _context.prediction
                .FirstOrDefaultAsync(p => p.PredictionID == id && !p.IsDeleted);
        }

        public async Task AddAsync(Prediction prediction)
        {
            prediction.PredictionID = Guid.NewGuid();
            await CreateAsync(prediction);
        }

        public async Task UpdateAsync(Prediction prediction)
        {
            await UpdateAsync(prediction);
        }

        public async Task DeleteAsync(Guid id)
        {
            var prediction = await GetByIdAsync(id);
            if (prediction != null)
            {
                prediction.IsDeleted = true;
                await UpdateAsync(prediction);
            }
        }
    }
}
