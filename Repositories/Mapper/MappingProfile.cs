using Applications.DTO.Create;
using Applications.DTO.Response;
using AutoMapper;
using Repositories.Models;

namespace Applications.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Grade mappings
            CreateMap<Grade, GradeDto>()
                .ForMember(dest => dest.Student, opt => opt.MapFrom(src => src.Student))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course))
                .ForMember(dest => dest.GradeDetails, opt => opt.MapFrom(src => src.GradeDetails));
            CreateMap<GradeCreateDto, Grade>();
            CreateMap<GradeDetail, GradeDetailDto>();
            CreateMap<GradeDetailCreateDto, GradeDetail>();
            CreateMap<Grade, GradeWithCourseSemesterDto>()
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course))
                .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => src.Course.Semester));

            // Attendance mappings
            CreateMap<Attendance, AttendanceDto>();
            CreateMap<AttendanceCreateDto, Attendance>();
            // Attendance with Course & Semester mapping
            CreateMap<Attendance, AttendanceWithCourseSemesterDto>()
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Enrollment.Course))
                .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => src.Enrollment.Course.Semester));


            // Course mappings
            CreateMap<Course, CourseDto>();
            CreateMap<CourseCreateDto, Course>();

            // Curriculum mappings
            CreateMap<Curriculum, CurriculumDto>();
            CreateMap<CurriculumCreateDto, Curriculum>();

            // Enrollment mappings
            CreateMap<Enrollment, EnrollmentDto>();
            CreateMap<EnrollmentCreateDto, Enrollment>();

            // Major mappings
            CreateMap<Major, MajorDto>();
            CreateMap<MajorCreateDto, Major>();

            // Notification mappings
            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationCreateDto, Notification>();
            CreateMap<Notification, NotificationWithCourseSemesterDto>();



            // Prediction mappings
            CreateMap<Prediction, PredictionDto>();
            CreateMap<PredictionCreateDto, Prediction>();

            // RiskAnalysis mappings
            CreateMap<RiskAnalysis, RiskAnalysisDto>();
            CreateMap<RiskAnalysisCreateDto, RiskAnalysis>();

            // Semester mappings
            CreateMap<Semester, SemesterDto>();
            CreateMap<SemesterCreateDto, Semester>();

            // Subject mappings
            CreateMap<Subject, SubjectDto>();
            CreateMap<SubjectCreateDto, Subject>();

            // Suggestion mappings
            CreateMap<Suggestion, SuggestionDto>();
            CreateMap<SuggestionCreateDto, Suggestion>();

            // Syllabus mappings
            CreateMap<Syllabus, SyllabusDto>();
            CreateMap<SyllabusCreateDto, Syllabus>();

            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<UserCreateDto, User>();
    //.ForMember(dest => dest.Password, opt => opt.Ignore());

        }
    }
}