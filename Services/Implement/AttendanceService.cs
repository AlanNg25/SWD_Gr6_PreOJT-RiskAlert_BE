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
    public class AttendanceService : IAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttendanceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AttendanceDto>> GetAllAsync()
        {
            var attendances = await _unitOfWork.AttendanceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AttendanceDto>>(attendances);
        }

        public async Task<AttendanceDto> GetByIdAsync(Guid id)
        {
            var attendance = await _unitOfWork.AttendanceRepository.GetByIdAsync(id);
            if (attendance == null)
                throw new KeyNotFoundException("Attendance not found");
            return _mapper.Map<AttendanceDto>(attendance);
        }

        public async Task AddAsync(AttendanceCreateDto attendanceDto)
        {
            var attendance = _mapper.Map<Attendance>(attendanceDto);
            await _unitOfWork.AttendanceRepository.AddAsync(attendance);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task UpdateAsync(Guid id, AttendanceCreateDto attendanceDto)
        {
            var existingAttendance = await _unitOfWork.AttendanceRepository.GetByIdAsync(id);
            if (existingAttendance == null)
                throw new KeyNotFoundException("Attendance not found");

            var updatedAttendance = _mapper.Map(attendanceDto, existingAttendance);
            updatedAttendance.AttendanceID = id;
            await _unitOfWork.AttendanceRepository.UpdateAsync(updatedAttendance);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.AttendanceRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }
    }
}
