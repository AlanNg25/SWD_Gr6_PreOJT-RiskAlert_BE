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
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            var courses = await _unitOfWork.CourseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto> GetByIdAsync(Guid id)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(id);
            if (course == null)
                throw new KeyNotFoundException("Course not found");
            return _mapper.Map<CourseDto>(course);
        }

        public async Task AddAsync(CourseCreateDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            await _unitOfWork.CourseRepository.AddAsync(course);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task UpdateAsync(Guid id, CourseCreateDto courseDto)
        {
            var existingCourse = await _unitOfWork.CourseRepository.GetByIdAsync(id);
            if (existingCourse == null)
                throw new KeyNotFoundException("Course not found");

            var updatedCourse = _mapper.Map(courseDto, existingCourse);
            updatedCourse.CourseID = id;
            await _unitOfWork.CourseRepository.UpdateAsync(updatedCourse);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.CourseRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesWithTransactionAsync();
        }
    }
}
