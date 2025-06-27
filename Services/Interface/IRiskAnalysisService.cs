using Applications.DTO.Create;
using Applications.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IRiskAnalysisService
    {
        Task<IEnumerable<RiskAnalysisDto>> GetAllAsync();
        Task<RiskAnalysisDto> GetByIdAsync(Guid id);
        Task AddAsync(RiskAnalysisCreateDto riskAnalysisDto);
        Task UpdateAsync(Guid id, RiskAnalysisCreateDto riskAnalysisDto);
        Task DeleteAsync(Guid id);
    }
}
