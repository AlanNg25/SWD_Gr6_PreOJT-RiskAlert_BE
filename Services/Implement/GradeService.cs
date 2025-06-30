using AutoMapper;
using Repositories;
using Repositories.Models;
using Applications.DTO.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repositories.Interfaces;
using Services.Interface;
using Repositories.Repositories;

namespace Services.Implement
{
    public class GradeService : IGradeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GradeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GradeDto> GetByIdAsync(Guid id)
        {
            var grade = await _unitOfWork.GradeRepository.GetGradeByIdAsync(id);
            if (grade == null)
                throw new KeyNotFoundException("Grade not found");
            return _mapper.Map<GradeDto>(grade);
        }

        public async Task<IEnumerable<GradeDto>> GetAllAsync()
        {
            var grades = await _unitOfWork.GradeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GradeDto>>(grades);
        }

        public async Task AddAsync(Grade grade)
        {
            if (grade.ScoreAverage < 0 || grade.ScoreAverage > 10)
                throw new ArgumentException("Score average must be between 0 and 10");
            await _unitOfWork.GradeRepository.AddAsync(grade);
        }

        public async Task UpdateAsync(Grade grade)
        {
            if (grade.ScoreAverage < 0 || grade.ScoreAverage > 10)
                throw new ArgumentException("Score average must be between 0 and 10");
            await _unitOfWork.GradeRepository.UpdateAsync(grade);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.GradeRepository.DeleteAsync(id);
        }

        public async Task<GradeDetailDto> GetGradeDetailByIdAsync(Guid id)
        {
            var gradeDetail = await _unitOfWork.GradeRepository.GetGradeDetailByIdAsync(id);
            if (gradeDetail == null)
                throw new KeyNotFoundException("GradeDetail not found");
            return _mapper.Map<GradeDetailDto>(gradeDetail);
        }

        public async Task<IEnumerable<GradeDetailDto>> GetGradeDetailsByGradeIdAsync(Guid gradeId)
        {
            var gradeDetails = await _unitOfWork.GradeRepository.GetGradeDetailsByGradeIdAsync(gradeId);
            return _mapper.Map<IEnumerable<GradeDetailDto>>(gradeDetails);
        }

        public async Task AddGradeDetailAsync(GradeDetail gradeDetail)
        {
            if (gradeDetail.Score < 0 || gradeDetail.Score > 10)
                throw new ArgumentException("Score must be between 0 and 10");
            if (gradeDetail.ScoreWeight < 0 || gradeDetail.ScoreWeight > 1)
                throw new ArgumentException("Score weight must be between 0 and 1");
            await _unitOfWork.GradeRepository.AddGradeDetailAsync(gradeDetail);
            await RecalculateScoreAverageAsync(gradeDetail.GradeID);
        }

        public async Task UpdateGradeDetailAsync(GradeDetail gradeDetail)
        {
            if (gradeDetail.Score < 0 || gradeDetail.Score > 10)
                throw new ArgumentException("Score must be between 0 and 10");
            if (gradeDetail.ScoreWeight < 0 || gradeDetail.ScoreWeight > 1)
                throw new ArgumentException("Score weight must be between 0 and 1");
            await _unitOfWork.GradeRepository.UpdateGradeDetailAsync(gradeDetail);
            await RecalculateScoreAverageAsync(gradeDetail.GradeID);
        }

        public async Task DeleteGradeDetailAsync(Guid id)
        {
            await _unitOfWork.GradeRepository.DeleteGradeDetailAsync(id);
        }

        private async Task RecalculateScoreAverageAsync(Guid gradeId)
        {
            // Lấy toàn bộ GradeDetail của Grade này
            var gradeDetails = await _unitOfWork.GradeRepository.GetGradeDetailsByGradeIdAsync(gradeId);
            if (gradeDetails == null || !gradeDetails.Any()) return;

            // ► Nếu có ScoreWeight thì tính trung bình có trọng số,
            //   ngược lại dùng trung bình cộng đơn giản.
            decimal average;
            var totalWeight = gradeDetails.Sum(d => d.ScoreWeight);

            if (totalWeight > 0)
            {
                var weightedSum = gradeDetails.Sum(d => d.Score * d.ScoreWeight);
                average = weightedSum / totalWeight;
            }
            else
            {
                average = gradeDetails.Average(d => d.Score);
            }

            // Cập nhật vào Grade
            var grade = await _unitOfWork.GradeRepository.GetGradeByIdAsync(gradeId);
            if (grade == null) return;

            grade.ScoreAverage = Math.Round(average, 2);      // làm tròn 2 chữ số thập phân (tuỳ bạn)
            await _unitOfWork.GradeRepository.UpdateAsync(grade);        // đã có sẵn trong repo
        }

        public async Task<IEnumerable<GradeDto>> GetByUserIdAsync(Guid userId)
        {
            var grades = await _unitOfWork.GradeRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<GradeDto>>(grades);
        }


    }
}