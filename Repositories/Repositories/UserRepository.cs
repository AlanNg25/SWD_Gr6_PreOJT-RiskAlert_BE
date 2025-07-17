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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(RiskAlertDBContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.user.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.user
                .Where(u => !u.IsDeleted).OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.user
                .FirstOrDefaultAsync(u => u.UserID == id && !u.IsDeleted);
        }

        public async Task AddAsync(User user)
        {
            user.UserID = Guid.NewGuid();
            await CreateAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            // gọi phương thức Update từ GenericRepository
            await base.UpdateAsync(user);
        }


        public async Task DeleteAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                user.IsDeleted = true;
                await UpdateAsync(user);
            }
        }

        public async Task<IEnumerable<User>> GetByRoleAsync(string role)
        {
            return await _context.user
                .Where(u => u.Role == role && !u.IsDeleted).OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }
    }
}
