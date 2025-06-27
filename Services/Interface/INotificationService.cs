using Applications.DTO.Create;
using Applications.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetAllAsync();
        Task<NotificationDto> GetByIdAsync(Guid id);
        Task AddAsync(NotificationCreateDto notificationDto);
        Task UpdateAsync(Guid id, NotificationCreateDto notificationDto);
        Task DeleteAsync(Guid id);
    }
}
