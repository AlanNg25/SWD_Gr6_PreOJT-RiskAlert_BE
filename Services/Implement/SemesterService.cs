using Applications.DTO.Response;
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
        private readonly SemesterRepository _repository;
        public SemesterService(SemesterRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateSemesterAsync(SemesterDto semester)
        {
            return await _repository.CreateSemesterAsync(semester);
        }

        public async Task<int> DeleteSemesterAsync(Guid semesterId)
        {
            return await _repository.DeleteSemesterAsync(semesterId);
        }

        public async Task<List<SemesterDto>> GetAllSemestersAsync()
        {
            return await _repository.GetAllSemestersAsync();
        }

        public async Task<SemesterDto> GetSemesterByIdAsync(Guid semesterId)
        {
            return await _repository.GetSemesterByIdAsync(semesterId);
        }

        public async Task<List<SemesterDto>> SearchSemesterAsync(string SemesterCode)
        {
            return await _repository.SearchSemesterAsync(SemesterCode);
        }

        public async Task<int> UpdateSemesterAsync(SemesterDto semester)
        {
            return await _repository.UpdateSemesterAsync(semester);
        }
    }
}
