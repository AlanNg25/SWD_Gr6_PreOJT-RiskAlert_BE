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
    public class GradeRepository : GenericRepository<Grade>, IGradeRepository
    {
        public GradeRepository(RiskAlertDBContext context) : base(context)
        {
        }

        public async Task<Grade> GetGradeByIdAsync(Guid id)
        {
            return await _context.grade
                .Include(g => g.GradeDetails)
                .Include(g => g.Student)
                .Include(g => g.Course)
                .FirstOrDefaultAsync(g => g.GradeID == id && !g.IsDeleted);
        }

        public async Task<IEnumerable<Grade>> GetAllAsync()
        {
            return await _context.grade
                .Include(g => g.GradeDetails)
                .Include(g => g.Student)
                .Include(g => g.Course)
                .Where(g => !g.IsDeleted)
                .ToListAsync();
        }

        public async Task AddAsync(Grade grade)
        {
            grade.GradeID = Guid.NewGuid();
            grade.IsDeleted = false;
            await base.CreateAsync(grade);
        }

        public async Task UpdateAsync(Grade grade)
        {
            await base.UpdateAsync(grade);
        }

        public async Task DeleteAsync(Guid id)
        {
            var grade = await GetByIdAsync(id);
            if (grade != null)
            {
                grade.IsDeleted = true;
                await base.UpdateAsync(grade);
            }
        }

        public async Task<GradeDetail> GetGradeDetailByIdAsync(Guid id)
        {
            return await _context.gradeDetail
                .FirstOrDefaultAsync(gd => gd.GradeDetailID == id && !gd.IsDeleted);
        }

        public async Task<IEnumerable<GradeDetail>> GetGradeDetailsByGradeIdAsync(Guid gradeId)
        {
            return await _context.gradeDetail
                .Where(gd => gd.GradeID == gradeId && !gd.IsDeleted)
                .ToListAsync();
        }

        public async Task AddGradeDetailAsync(GradeDetail gradeDetail)
        {
            gradeDetail.GradeDetailID = Guid.NewGuid();
            gradeDetail.IsDeleted = false;
            await _context.gradeDetail.AddAsync(gradeDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGradeDetailAsync(GradeDetail gradeDetail)
        {
            await _context.gradeDetail
                .Where(gd => gd.GradeDetailID == gradeDetail.GradeDetailID)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(gd => gd.GradeType, gradeDetail.GradeType)
                    .SetProperty(gd => gd.Score, gradeDetail.Score)
                    .SetProperty(gd => gd.ScoreWeight, gradeDetail.ScoreWeight)
                    .SetProperty(gd => gd.MinScr, gradeDetail.MinScr));
        }

        public async Task DeleteGradeDetailAsync(Guid id)
        {
            await _context.gradeDetail
                .Where(gd => gd.GradeDetailID == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(gd => gd.IsDeleted, true));
        }


        public async Task<IEnumerable<Grade>> GetByUserIdAsync(Guid userId)
        {
            return await _context.grade
                .Include(g => g.Course)
                    .ThenInclude(c => c.Semester)
                .Where(g => g.StudentID == userId && !g.IsDeleted && !g.Course.IsDeleted)
                .ToListAsync();
        }


    }
}
