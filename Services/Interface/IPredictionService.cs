using Applications.DTO.Create;
using Applications.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IPredictionService
    {
        Task<IEnumerable<PredictionDto>> GetAllAsync();
        Task<PredictionDto> GetByIdAsync(Guid id);
        Task AddAsync(PredictionCreateDto predictionDto);
        Task UpdateAsync(Guid id, PredictionCreateDto predictionDto);
        Task DeleteAsync(Guid id);
    }
}
