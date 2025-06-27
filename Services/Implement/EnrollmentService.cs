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
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EnrollmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EnrollmentDto>> GetAllAsync()
        {
            var enrollments = await _unitOfWork.EnrollmentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
        }

        public async Task<EnrollmentDto> GetByIdAsync(Guid id)
        {
            var enrollment = await _unitOfWork.EnrollmentRepository.GetByIdAsync(id);
            if (enrollment == null)
                throw new KeyNotFoundException("Enrollment not found");
            return _mapper.Map<EnrollmentDto>(enrollment);
        }

        public async Task AddAsync(EnrollmentCreateDto enrollmentDto)
        {
            var enrollment = _mapper.Map<Enrollment>(enrollmentDto);
            await _unitOfWork.EnrollmentRepository.AddAsync(enrollment);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task UpdateAsync(Guid id, EnrollmentCreateDto enrollmentDto)
        {
            var existingEnrollment = await _unitOfWork.EnrollmentRepository.GetByIdAsync(id);
            if (existingEnrollment == null)
                throw new KeyNotFoundException("Enrollment not found");

            var updatedEnrollment = _mapper.Map(enrollmentDto, existingEnrollment);
            updatedEnrollment.EnrollmentID = id;
            await _unitOfWork.EnrollmentRepository.UpdateAsync(updatedEnrollment);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.EnrollmentRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }
    }
}
