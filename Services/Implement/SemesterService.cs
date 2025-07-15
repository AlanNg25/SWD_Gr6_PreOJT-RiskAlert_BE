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
    public class SemesterService : ISemesterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SemesterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> CreateSemesterAsync(SemesterCreateDto semesterDto)
        {
            var semester = _mapper.Map<Semester>(semesterDto);
            semester.SemesterID = Guid.NewGuid();
            semester.IsDeleted = false;

            return await _unitOfWork.SemesterRepository.CreateSemesterAsync(semester);
        }


        public async Task<int> DeleteSemesterAsync(Guid semesterId)
        {
            return await _unitOfWork.SemesterRepository.DeleteSemesterAsync(semesterId);
        }

        public async Task<List<SemesterDto>> GetAllSemestersAsync()
        {
            return await _unitOfWork.SemesterRepository.GetAllSemestersAsync();
        }

        public async Task<SemesterDto> GetSemesterByIdAsync(Guid semesterId)
        {
            return await _unitOfWork.SemesterRepository.GetSemesterByIdAsync(semesterId);
        }

        public async Task<List<SemesterDto>> SearchSemesterAsync(string SemesterCode)
        {
            return await _unitOfWork.SemesterRepository.SearchSemesterAsync(SemesterCode);
        }

        public async Task<int> UpdateSemesterAsync(Guid semesterId, SemesterCreateDto semesterDto)
        {
            var existing = await _unitOfWork.SemesterRepository.GetSemesterByIdRawAsync(semesterId);
            if (existing == null || existing.IsDeleted) return 0;

            var updatedSemester = _mapper.Map(semesterDto, existing);
            return await _unitOfWork.SemesterRepository.UpdateSemesterAsync(updatedSemester);
        }
    }
}
