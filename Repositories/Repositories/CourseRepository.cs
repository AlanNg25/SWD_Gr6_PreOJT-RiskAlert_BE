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
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(RiskAlertDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.course
            .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Course> GetByIdAsync(Guid id)
        {
            return await _context.course
                .FirstOrDefaultAsync(c => c.CourseID == id && !c.IsDeleted);
        }

        public async Task AddAsync(Course course)
        {
            course.CourseID = Guid.NewGuid();
            await CreateAsync(course);
        }

        public async Task UpdateAsync(Course course)
        {
            await UpdateAsync(course);
        }

        public async Task DeleteAsync(Guid id)
        {
            var course = await GetByIdAsync(id);
            if (course != null)
            {
                course.IsDeleted = true;
                await UpdateAsync(course);
            }
        }
    }
}
