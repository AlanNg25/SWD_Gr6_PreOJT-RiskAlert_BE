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
    public class RiskAnalysisService : IRiskAnalysisService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RiskAnalysisService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RiskAnalysisDto>> GetAllAsync()
        {
            var riskAnalyses = await _unitOfWork.RiskAnalysisRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RiskAnalysisDto>>(riskAnalyses);
        }

        public async Task<RiskAnalysisDto> GetByIdAsync(Guid id)
        {
            var riskAnalysis = await _unitOfWork.RiskAnalysisRepository.GetByIdAsync(id);
            if (riskAnalysis == null)
                throw new KeyNotFoundException("RiskAnalysis not found");
            return _mapper.Map<RiskAnalysisDto>(riskAnalysis);
        }

        public async Task AddAsync(RiskAnalysisCreateDto riskAnalysisDto)
        {
            var riskAnalysis = _mapper.Map<RiskAnalysis>(riskAnalysisDto);
            await _unitOfWork.RiskAnalysisRepository.AddAsync(riskAnalysis);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task UpdateAsync(Guid id, RiskAnalysisCreateDto riskAnalysisDto)
        {
            var existingRiskAnalysis = await _unitOfWork.RiskAnalysisRepository.GetByIdAsync(id);
            if (existingRiskAnalysis == null)
                throw new KeyNotFoundException("RiskAnalysis not found");

            var updatedRiskAnalysis = _mapper.Map(riskAnalysisDto, existingRiskAnalysis);
            updatedRiskAnalysis.RiskID = id;
            await _unitOfWork.RiskAnalysisRepository.UpdateAsync(updatedRiskAnalysis);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.RiskAnalysisRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }
    }
}
