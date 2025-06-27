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
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(RiskAlertDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.subject
                .Where(s => !s.IsDeleted)
                .ToListAsync();
        }

        public async Task<Subject> GetByIdAsync(Guid id)
        {
            return await _context.subject
                .FirstOrDefaultAsync(s => s.SubjectID == id && !s.IsDeleted);
        }

        public async Task AddAsync(Subject subject)
        {
            subject.SubjectID = Guid.NewGuid();
            await CreateAsync(subject);
        }

        public async Task UpdateAsync(Subject subject)
        {
            await UpdateAsync(subject);
        }

        public async Task DeleteAsync(Guid id)
        {
            var subject = await GetByIdAsync(id);
            if (subject != null)
            {
                subject.IsDeleted = true;
                await UpdateAsync(subject);
            }
        }
    }
}
