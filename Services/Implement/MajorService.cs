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
    public class MajorService : IMajorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MajorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MajorDto>> GetAllAsync()
        {
            var majors = await _unitOfWork.MajorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MajorDto>>(majors);
        }

        public async Task<MajorDto> GetByIdAsync(Guid id)
        {
            var major = await _unitOfWork.MajorRepository.GetByIdAsync(id);
            if (major == null)
                throw new KeyNotFoundException("Major not found");
            return _mapper.Map<MajorDto>(major);
        }

        public async Task AddAsync(MajorCreateDto majorDto)
        {
            var major = _mapper.Map<Major>(majorDto);
            await _unitOfWork.MajorRepository.AddAsync(major);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task UpdateAsync(Guid id, MajorCreateDto majorDto)
        {
            var existingMajor = await _unitOfWork.MajorRepository.GetByIdAsync(id);
            if (existingMajor == null)
                throw new KeyNotFoundException("Major not found");

            var updatedMajor = _mapper.Map(majorDto, existingMajor);
            updatedMajor.MajorID = id;
            await _unitOfWork.MajorRepository.UpdateAsync(updatedMajor);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.MajorRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }
    }
}
