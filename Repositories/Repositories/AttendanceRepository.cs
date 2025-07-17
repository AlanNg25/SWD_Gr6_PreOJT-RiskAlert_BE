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
    

    public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(RiskAlertDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Attendance>> GetAllAsync()
        {
            return await _context.attendance
                .Include(a => a.Enrollment)
                .Where(a => !a.IsDeleted).OrderByDescending(a => a.Enrollment.EnrollmentDate)
            .ToListAsync();
        }

        public async Task<Attendance> GetByIdAsync(Guid id)
        {
            return await _context.attendance
                .FirstOrDefaultAsync(a => a.AttendanceID == id && !a.IsDeleted);
        }

        public async Task AddAsync(Attendance attendance)
        {
            attendance.AttendanceID = Guid.NewGuid();
            await CreateAsync(attendance);
        }

        public async Task UpdateAsync(Attendance attendance)
        {
            await base.UpdateAsync(attendance);
        }

        public async Task DeleteAsync(Guid id)
        {
            var attendance = await GetByIdAsync(id);
            if (attendance != null)
            {
                attendance.IsDeleted = true;
                await UpdateAsync(attendance);
            }
        }

        public async Task<IEnumerable<Attendance>> GetByUserIdAsync(Guid userId)
        {
            return await _context.attendance
                .Include(a => a.Enrollment)
                    .ThenInclude(e => e.Course)
                        .ThenInclude(c => c.Semester)
                .Where(a => a.Enrollment.StudentID == userId && !a.IsDeleted && !a.Enrollment.IsDeleted && !a.Enrollment.Course.IsDeleted).OrderByDescending(a => a.Enrollment.EnrollmentDate)
                .ToListAsync();
        }

    }
}
