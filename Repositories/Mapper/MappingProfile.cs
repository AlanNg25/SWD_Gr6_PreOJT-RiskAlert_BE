using Applications.DTO.Create;
using Applications.DTO.Response;
using AutoMapper;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Applications.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map Grade to GradeDto
            CreateMap<Grade, GradeDto>()
                .ForMember(dest => dest.Student, opt => opt.MapFrom(src => src.Student))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course))
                .ForMember(dest => dest.GradeDetails, opt => opt.MapFrom(src => src.GradeDetails));

            // Map GradeCreateDto → Grade (for create)
            CreateMap<GradeCreateDto, Grade>();

            // Map User to UserDto, ignoring cyclic references
            CreateMap<User, UserDto>(); // Prevent cycle

            // Map Course to CourseDto, ignoring cyclic references
            CreateMap<Course, CourseDto>(); // Prevent cycle

            // Map GradeDetail to GradeDetailDto
            CreateMap<GradeDetail, GradeDetailDto>();
        }
    }
}
