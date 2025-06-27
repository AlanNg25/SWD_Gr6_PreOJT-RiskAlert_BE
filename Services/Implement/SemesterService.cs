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

        public async Task<int> CreateSemesterAsync(SemesterDto semester)
        {
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

        public async Task<int> UpdateSemesterAsync(SemesterDto semester)
        {
            return await _unitOfWork.SemesterRepository.UpdateSemesterAsync(semester);
        }
    }
}
