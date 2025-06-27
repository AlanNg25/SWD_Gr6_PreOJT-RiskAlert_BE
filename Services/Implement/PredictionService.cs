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
    public class PredictionService : IPredictionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PredictionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PredictionDto>> GetAllAsync()
        {
            var predictions = await _unitOfWork.PredictionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PredictionDto>>(predictions);
        }

        public async Task<PredictionDto> GetByIdAsync(Guid id)
        {
            var prediction = await _unitOfWork.PredictionRepository.GetByIdAsync(id);
            if (prediction == null)
                throw new KeyNotFoundException("Prediction not found");
            return _mapper.Map<PredictionDto>(prediction);
        }

        public async Task AddAsync(PredictionCreateDto predictionDto)
        {
            var prediction = _mapper.Map<Prediction>(predictionDto);
            await _unitOfWork.PredictionRepository.AddAsync(prediction);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task UpdateAsync(Guid id, PredictionCreateDto predictionDto)
        {
            var existingPrediction = await _unitOfWork.PredictionRepository.GetByIdAsync(id);
            if (existingPrediction == null)
                throw new KeyNotFoundException("Prediction not found");

            var updatedPrediction = _mapper.Map(predictionDto, existingPrediction);
            updatedPrediction.PredictionID = id;
            await _unitOfWork.PredictionRepository.UpdateAsync(updatedPrediction);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.PredictionRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }
    }
}
