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
    public class SyllabusService : ISyllabusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SyllabusService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SyllabusDto>> GetAllAsync()
        {
            var syllabuses = await _unitOfWork.SyllabusRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SyllabusDto>>(syllabuses);
        }

        public async Task<SyllabusDto> GetByIdAsync(Guid id)
        {
            var syllabus = await _unitOfWork.SyllabusRepository.GetByIdAsync(id);
            if (syllabus == null)
                throw new KeyNotFoundException("Syllabus not found");
            return _mapper.Map<SyllabusDto>(syllabus);
        }

        public async Task AddAsync(SyllabusCreateDto syllabusDto)
        {
            var syllabus = _mapper.Map<Syllabus>(syllabusDto);
            await _unitOfWork.SyllabusRepository.AddAsync(syllabus);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task UpdateAsync(Guid id, SyllabusCreateDto syllabusDto)
        {
            var existingSyllabus = await _unitOfWork.SyllabusRepository.GetByIdAsync(id);
            if (existingSyllabus == null)
                throw new KeyNotFoundException("Syllabus not found");

            var updatedSyllabus = _mapper.Map(syllabusDto, existingSyllabus);
            updatedSyllabus.SyllabusID = id;
            await _unitOfWork.SyllabusRepository.UpdateAsync(updatedSyllabus);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.SyllabusRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }
    }
}
