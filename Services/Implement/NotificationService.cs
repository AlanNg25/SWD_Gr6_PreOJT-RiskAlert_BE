using Applications.DTO.Create;
using Applications.DTO.Response;
using AutoMapper;
using Repositories.Models;
using Repositories.Repositories;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificationDto>> GetAllAsync()
        {
            var notifications = await _unitOfWork.NotificationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }

        public async Task<NotificationDto> GetByIdAsync(Guid id)
        {
            var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(id);
            if (notification == null)
                throw new KeyNotFoundException("Notification not found");
            return _mapper.Map<NotificationDto>(notification);
        }

        public async Task AddAsync(NotificationCreateDto notificationDto)
        {
            var notification = _mapper.Map<Notification>(notificationDto);
            await _unitOfWork.NotificationRepository.AddAsync(notification);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task UpdateAsync(Guid id, NotificationCreateDto notificationDto)
        {
            var existingNotification = await _unitOfWork.NotificationRepository.GetByIdAsync(id);
            if (existingNotification == null)
                throw new KeyNotFoundException("Notification not found");

            var updatedNotification = _mapper.Map(notificationDto, existingNotification);
            updatedNotification.NotificationID = id;
            await _unitOfWork.NotificationRepository.UpdateAsync(updatedNotification);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.NotificationRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task<IEnumerable<NotificationWithCourseSemesterDto>> GetByUserIdAsync(Guid userId)
        {
            var notifications = await _unitOfWork.NotificationRepository.GetByUserIdAsync(userId);

            var result = new List<NotificationWithCourseSemesterDto>();

            foreach (var n in notifications)
            {
                var dto = _mapper.Map<NotificationWithCourseSemesterDto>(n);

                var enrollment = n.Receiver?.Enrollments
                    .FirstOrDefault(e => !e.IsDeleted && e.Course != null && !e.Course.IsDeleted);

                if (enrollment?.Course != null)
                {
                    dto.Course = _mapper.Map<CourseDto>(enrollment.Course);
                    dto.Semester = _mapper.Map<SemesterDto>(enrollment.Course.Semester);
                }

                result.Add(dto);
            }

            return result;
        }


    }
}
