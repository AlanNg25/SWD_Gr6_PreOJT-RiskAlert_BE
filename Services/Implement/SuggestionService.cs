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
    public class SuggestionService : ISuggestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SuggestionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SuggestionDto>> GetAllAsync()
        {
            var suggestions = await _unitOfWork.SuggestionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SuggestionDto>>(suggestions);
        }

        public async Task<SuggestionDto> GetByIdAsync(Guid id)
        {
            var suggestion = await _unitOfWork.SuggestionRepository.GetByIdAsync(id);
            if (suggestion == null)
                throw new KeyNotFoundException("Suggestion not found");
            return _mapper.Map<SuggestionDto>(suggestion);
        }

        public async Task AddAsync(SuggestionCreateDto suggestionDto)
        {
            var suggestion = _mapper.Map<Suggestion>(suggestionDto);
            await _unitOfWork.SuggestionRepository.AddAsync(suggestion);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task UpdateAsync(Guid id, SuggestionCreateDto suggestionDto)
        {
            var existingSuggestion = await _unitOfWork.SuggestionRepository.GetByIdAsync(id);
            if (existingSuggestion == null)
                throw new KeyNotFoundException("Suggestion not found");

            var updatedSuggestion = _mapper.Map(suggestionDto, existingSuggestion);
            updatedSuggestion.SuggestionID = id;
            await _unitOfWork.SuggestionRepository.UpdateAsync(updatedSuggestion);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.SuggestionRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }
    }
}
