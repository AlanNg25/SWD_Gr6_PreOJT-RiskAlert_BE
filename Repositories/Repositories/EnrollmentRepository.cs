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
    public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(RiskAlertDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await _context.enrollment
            .Where(e => !e.IsDeleted).OrderByDescending(e => e.EnrollmentDate)
                .ToListAsync();
        }

        public async Task<Enrollment> GetByIdAsync(Guid id)
        {
            return await _context.enrollment
                .FirstOrDefaultAsync(e => e.EnrollmentID == id && !e.IsDeleted);
        }

        public async Task AddAsync(Enrollment enrollment)
        {
            enrollment.EnrollmentID = Guid.NewGuid();
            enrollment.EnrollmentDate = DateTime.UtcNow;
            await CreateAsync(enrollment);
        }

        public async Task UpdateAsync(Enrollment enrollment)
        {
            await base.UpdateAsync(enrollment);
        }

        public async Task DeleteAsync(Guid id)
        {
            var enrollment = await GetByIdAsync(id);
            if (enrollment != null)
            {
                enrollment.IsDeleted = true;
                await UpdateAsync(enrollment);
            }
        }
    }
}
