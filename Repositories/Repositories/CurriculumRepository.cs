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
    public class CurriculumRepository : GenericRepository<Curriculum>, ICurriculumRepository
    {
        public CurriculumRepository(RiskAlertDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Curriculum>> GetAllAsync()
        {
            return await _context.curriculum
            .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Curriculum> GetByIdAsync(Guid id)
        {
            return await _context.curriculum
                .FirstOrDefaultAsync(c => c.CurriculumID == id && !c.IsDeleted);
        }

        public async Task AddAsync(Curriculum curriculum)
        {
            curriculum.CurriculumID = Guid.NewGuid();
            await CreateAsync(curriculum);
        }

        public async Task UpdateAsync(Curriculum curriculum)
        {
            await base.UpdateAsync(curriculum);
        }

        public async Task DeleteAsync(Guid id)
        {
            var curriculum = await GetByIdAsync(id);
            if (curriculum != null)
            {
                curriculum.IsDeleted = true;
                await UpdateAsync(curriculum);
            }
        }
    }
}
