using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenderHealthcare.Repositories.ThangHN.Basic;
using Microsoft.EntityFrameworkCore;
using Repositories.DBContext;
using Repositories.Models;

namespace Repositories.Repositories
{
    public class ClassRepository : GenericRepository<Classes>
    {
        public ClassRepository() { }
        public ClassRepository(RiskAlertDBContext context) => _context = context;
        public async Task<List<Classes>> GetAllClassesAsync()
        {
            var items = await _context.classes
                .Where(c => !c.IsDeleted)
                .ToListAsync();
            return items ?? new List<Classes>();
        }
        public async Task<Classes> GetClassByIdAsync(Guid classId)
        {
            var item = await _context.classes
                .FirstOrDefaultAsync(c => c.ClassID == classId && !c.IsDeleted);
            return item ?? new Classes();
        }
        public async Task<int> CreateClassAsync(Classes classes)
        {
            _context.classes.Add(classes);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateClassAsync(Classes classes)
        {
            _context.ChangeTracker.Clear();
            var tracker = _context.Attach(classes);
            tracker.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteClassAsync(Guid classId)
        {
            var item = await _context.classes
                .FirstOrDefaultAsync(c => c.ClassID == classId && !c.IsDeleted);
            if (item != null)
            {
                item.IsDeleted = true;
                return await _context.SaveChangesAsync();
            }
            return 0; // No changes made if class not found
        }
        public async Task<List<Classes>> SearchClassAsync(string classCode, string className)
        {
            var items = await _context.classes
                .Where(c => !c.IsDeleted &&
                            (string.IsNullOrEmpty(classCode) || c.ClassCode.Contains(classCode)) &&
                            (string.IsNullOrEmpty(className) || c.ClassName.Contains(className)))
                .ToListAsync();
            return items ?? new List<Classes>();
        }
    }
}
