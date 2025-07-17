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
                }).OrderByDescending(s => s.StartDate)
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
        public async Task<Guid> CreateSemesterAsync(Semester semester)
        {
            _context.semester.Add(semester);
            await _context.SaveChangesAsync();
            return semester.SemesterID;
        }

        public async Task<int> UpdateSemesterAsync(Semester semester)
        {
            _context.semester.Update(semester);
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
                }).OrderByDescending(s => s.StartDate)
                .ToListAsync();
            return items ?? new List<SemesterDto>();
        }

        public async Task<Semester> GetSemesterByIdRawAsync(Guid semesterId)
        {
            return await _context.semester.FirstOrDefaultAsync(s => s.SemesterID == semesterId && !s.IsDeleted);
        }

    }
}
