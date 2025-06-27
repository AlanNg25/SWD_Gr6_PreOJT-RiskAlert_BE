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
    public class SyllabusRepository : GenericRepository<Syllabus>, ISyllabusRepository
    {
        public SyllabusRepository(RiskAlertDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Syllabus>> GetAllAsync()
        {
            return await _context.syllabus
            .Where(s => !s.IsDeleted)
                .ToListAsync();
        }

        public async Task<Syllabus> GetByIdAsync(Guid id)
        {
            return await _context.syllabus
                .FirstOrDefaultAsync(s => s.SyllabusID == id && !s.IsDeleted);
        }

        public async Task AddAsync(Syllabus syllabus)
        {
            syllabus.SyllabusID = Guid.NewGuid();
            await CreateAsync(syllabus);
        }

        public async Task UpdateAsync(Syllabus syllabus)
        {
            await UpdateAsync(syllabus);
        }

        public async Task DeleteAsync(Guid id)
        {
            var syllabus = await GetByIdAsync(id);
            if (syllabus != null)
            {
                syllabus.IsDeleted = true;
                await UpdateAsync(syllabus);
            }
        }
    }
}
