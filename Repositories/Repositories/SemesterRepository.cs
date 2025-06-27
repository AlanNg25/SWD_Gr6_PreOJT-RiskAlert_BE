using Applications.DTO.Response;
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
    public class SemesterRepository : GenericRepository<Course>, ISemesterRepository
    {
        public SemesterRepository() { }
        public SemesterRepository(RiskAlertDBContext context) => _context = context;
        public async Task<List<SemesterDto>> GetAllSemestersAsync()
        {
            var items = await _context.semester
                .Where(s => !s.IsDeleted)
                .Select(s => new SemesterDto 
                {
                    SemesterID = s.SemesterID,
                    SemesterCode = s.SemesterCode,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    IsDeleted = s.IsDeleted,
                })
                .ToListAsync();
            return items ?? new List<SemesterDto>();
        }
        public async Task<SemesterDto> GetSemesterByIdAsync(Guid semesterId)
        {
            var item = await _context.semester
                .Select(s => new SemesterDto
                {
                    SemesterID = s.SemesterID,
                    SemesterCode = s.SemesterCode,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    IsDeleted = s.IsDeleted,
                })
                .FirstOrDefaultAsync(s => s.SemesterID == semesterId && !s.IsDeleted);
            return item ?? new SemesterDto();
        }
        public async Task<int> CreateSemesterAsync(SemesterDto semester)
        {
            var item = new Semester
            {
                SemesterID = Guid.NewGuid(),
                SemesterCode = semester.SemesterCode,
                StartDate = semester.StartDate,
                EndDate = semester.EndDate,
                IsDeleted = semester.IsDeleted
            };
            _context.semester.Add(item);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateSemesterAsync(SemesterDto semester)
        {
            var item = new Semester
            {
                SemesterID = semester.SemesterID,
                SemesterCode = semester.SemesterCode,
                StartDate = semester.StartDate,
                EndDate = semester.EndDate,
                IsDeleted = semester.IsDeleted
            };
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
        public async Task<List<SemesterDto>> SearchSemesterAsync(string semesterCode)
        {
            var items = await _context.semester
                .Where(s => s.SemesterCode.ToLower().Contains(semesterCode.ToLower()) && !s.IsDeleted)
                .Select(s => new SemesterDto
                {
                    SemesterID = s.SemesterID,
                    SemesterCode = s.SemesterCode,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    IsDeleted = s.IsDeleted,
                })
                .ToListAsync();
            return items ?? new List<SemesterDto>();
        }
    }
}
