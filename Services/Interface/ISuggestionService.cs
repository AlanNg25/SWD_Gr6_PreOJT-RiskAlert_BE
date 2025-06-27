using Applications.DTO.Create;
using Applications.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ISuggestionService
    {
        Task<IEnumerable<SuggestionDto>> GetAllAsync();
        Task<SuggestionDto> GetByIdAsync(Guid id);
        Task AddAsync(SuggestionCreateDto suggestionDto);
        Task UpdateAsync(Guid id, SuggestionCreateDto suggestionDto);
        Task DeleteAsync(Guid id);
    }
}
