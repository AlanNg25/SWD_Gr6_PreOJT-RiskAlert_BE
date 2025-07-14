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
    public class MajorRepository : GenericRepository<Major>, IMajorRepository
    {
        public MajorRepository(RiskAlertDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Major>> GetAllAsync()
        {
            return await _context.major
            .Where(m => !m.IsDeleted)
                .ToListAsync();
        }

        public async Task<Major> GetByIdAsync(Guid id)
        {
            return await _context.major
                .FirstOrDefaultAsync(m => m.MajorID == id && !m.IsDeleted);
        }

        public async Task AddAsync(Major major)
        {
            major.MajorID = Guid.NewGuid();
            await CreateAsync(major);
        }

        public async Task UpdateAsync(Major major)
        {
            await base.UpdateAsync(major);
        }

        public async Task DeleteAsync(Guid id)
        {
            var major = await GetByIdAsync(id);
            if (major != null)
            {
                major.IsDeleted = true;
                await UpdateAsync(major);
            }
        }
    }
}
