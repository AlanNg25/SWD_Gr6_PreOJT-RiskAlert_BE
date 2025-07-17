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
    public class CurriculumService : ICurriculumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CurriculumService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CurriculumDto>> GetAllAsync()
        {
            var curriculums = await _unitOfWork.CurriculumRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CurriculumDto>>(curriculums);
        }

        public async Task<CurriculumDto> GetByIdAsync(Guid id)
        {
            var curriculum = await _unitOfWork.CurriculumRepository.GetByIdAsync(id);
            if (curriculum == null)
                throw new KeyNotFoundException("Curriculum not found");
            return _mapper.Map<CurriculumDto>(curriculum);
        }

        public async Task<CurriculumDto> GetBySubjectIdAsync(Guid subjectId)
        {
            var curriculums = await _unitOfWork.CurriculumRepository.GetBySubjectIdAsync(subjectId);
            if (curriculums == null)
                throw new KeyNotFoundException("Curriculum not found");
            return _mapper.Map<CurriculumDto>(curriculums);
        }

        public async Task AddAsync(CurriculumCreateDto curriculumDto)
        {
            var curriculum = _mapper.Map<Curriculum>(curriculumDto);
            await _unitOfWork.CurriculumRepository.AddAsync(curriculum);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task UpdateAsync(Guid id, CurriculumCreateDto curriculumDto)
        {
            var existingCurriculum = await _unitOfWork.CurriculumRepository.GetByIdAsync(id);
            if (existingCurriculum == null)
                throw new KeyNotFoundException("Curriculum not found");

            var updatedCurriculum = _mapper.Map(curriculumDto, existingCurriculum);
            updatedCurriculum.CurriculumID = id;
            await _unitOfWork.CurriculumRepository.UpdateAsync(updatedCurriculum);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.CurriculumRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }
    }
}
