using GenderHealthcare.Repositories.ThangHN.Basic;
using Microsoft.EntityFrameworkCore;
using Repositories.DBContext;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class SemesterRepository : GenericRepository<Course>
    {
        public SemesterRepository() { }
        public SemesterRepository(RiskAlertDBContext context) => _context = context;
        public async Task<List<Semester>> GetAllSemestersAsync()
        {
            var items = await _context.semester
                .Where(s => !s.IsDeleted)
                .ToListAsync();
            return items ?? new List<Semester>();
        }
        public async Task<Semester> GetSemesterByIdAsync(Guid semesterId)
        {
            var item = await _context.semester
                .FirstOrDefaultAsync(s => s.SemesterID == semesterId && !s.IsDeleted);
            return item ?? new Semester();
        }
        public async Task<int> CreateSemesterAsync(Semester semester)
        {
            _context.semester.Add(semester);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateSemesterAsync(Semester semester)
        {
            _context.ChangeTracker.Clear();
            var tracker = _context.Attach(semester);
            tracker.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteSemesterAsync(Guid semesterId)
        {
            var item = await _context.semester
                .FirstOrDefaultAsync(s => s.SemesterID == semesterId && !s.IsDeleted);
            if (item != null)
            {
                item.IsDeleted = true;
                return await _context.SaveChangesAsync();
            }
            return 0; // No changes made if semester not found
        }
        public async Task<List<Semester>> SearchSemesterAsync(string semesterName, string semesterCode)
        {
            var items = await _context.semester
                .Where(s => !s.IsDeleted && (string.IsNullOrEmpty(semesterCode) || s.SemesterCode.Contains(semesterCode)))
                .ToListAsync();
            return items ?? new List<Semester>();
        }
    }
}
