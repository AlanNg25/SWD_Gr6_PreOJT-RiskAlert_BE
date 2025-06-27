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
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubjectDto>> GetAllAsync()
        {
            var subjects = await _unitOfWork.SubjectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SubjectDto>>(subjects);
        }

        public async Task<SubjectDto> GetByIdAsync(Guid id)
        {
            var subject = await _unitOfWork.SubjectRepository.GetByIdAsync(id);
            if (subject == null)
                throw new KeyNotFoundException("Subject not found");
            return _mapper.Map<SubjectDto>(subject);
        }

        public async Task AddAsync(SubjectCreateDto subjectDto)
        {
            var subject = _mapper.Map<Subject>(subjectDto);
            await _unitOfWork.SubjectRepository.AddAsync(subject);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task UpdateAsync(Guid id, SubjectCreateDto subjectDto)
        {
            var existingSubject = await _unitOfWork.SubjectRepository.GetByIdAsync(id);
            if (existingSubject == null)
                throw new KeyNotFoundException("Subject not found");

            var updatedSubject = _mapper.Map(subjectDto, existingSubject);
            updatedSubject.SubjectID = id;
            await _unitOfWork.SubjectRepository.UpdateAsync(updatedSubject);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.SubjectRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }
    }
}
