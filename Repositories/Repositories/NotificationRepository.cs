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
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(RiskAlertDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await _context.notification
                .Where(n => !n.IsDeleted)
            .ToListAsync();
        }

        public async Task<Notification> GetByIdAsync(Guid id)
        {
            return await _context.notification
                .FirstOrDefaultAsync(n => n.NotificationID == id && !n.IsDeleted);
        }

        public async Task AddAsync(Notification notification)
        {
            notification.NotificationID = Guid.NewGuid();
            await CreateAsync(notification);
        }

        public async Task UpdateAsync(Notification notification)
        {
            await base.UpdateAsync(notification);
        }

        public async Task DeleteAsync(Guid id)
        {
            var notification = await GetByIdAsync(id);
            if (notification != null)
            {
                notification.IsDeleted = true;
                await UpdateAsync(notification);
            }
        }

        public async Task<IEnumerable<Notification>> GetByUserIdAsync(Guid userId)
        {
            return await _context.notification
                .Include(n => n.Receiver)
                .Include(n => n.Receiver.Enrollments.Where(e => !e.IsDeleted)) // filter trong navigation
                    .ThenInclude(e => e.Course)
                        .ThenInclude(c => c.Semester)
                .Where(n => n.ReceiverID == userId && !n.IsDeleted)
                .ToListAsync();
        }



    }
}
