using Repositories.Basic;
using Repositories.DBContext;
using Repositories.Interfaces;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAttendanceRepository AttendanceRepository { get; }
        ICourseRepository CourseRepository { get; }
        ICurriculumRepository CurriculumRepository { get; }
        IEnrollmentRepository EnrollmentRepository { get; }
        IGradeRepository GradeRepository { get; }
        IMajorRepository MajorRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IPredictionRepository PredictionRepository { get; }
        IRiskAnalysisRepository RiskAnalysisRepository { get; }
        ISemesterRepository SemesterRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        ISuggestionRepository SuggestionRepository { get; }
        ISyllabusRepository SyllabusRepository { get; }
        IUserRepository UserRepository { get; }

        int SaveChangesWithTransaction();
        Task<int> SaveChangesWithTransactionAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly RiskAlertDBContext _context;
        private IAttendanceRepository _attendanceRepository;
        private ICourseRepository _courseRepository;
        private ICurriculumRepository _curriculumRepository;
        private IEnrollmentRepository _enrollmentRepository;
        private IGradeRepository _gradeRepository;
        private IMajorRepository _majorRepository;
        private INotificationRepository _notificationRepository;
        private IPredictionRepository _predictionRepository;
        private IRiskAnalysisRepository _riskAnalysisRepository;
        private ISemesterRepository _semesterRepository;
        private ISubjectRepository _subjectRepository;
        private ISuggestionRepository _suggestionRepository;
        private ISyllabusRepository _syllabusRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(RiskAlertDBContext context)
        {
            _context = context;
        }

        public IAttendanceRepository AttendanceRepository
        {
            get { return _attendanceRepository ??= new AttendanceRepository(_context); }
        }

        public ICourseRepository CourseRepository
        {
            get { return _courseRepository ??= new CourseRepository(_context); }
        }

        public ICurriculumRepository CurriculumRepository
        {
            get { return _curriculumRepository ??= new CurriculumRepository(_context); }
        }

        public IEnrollmentRepository EnrollmentRepository
        {
            get { return _enrollmentRepository ??= new EnrollmentRepository(_context); }
        }

        public IGradeRepository GradeRepository
        {
            get { return _gradeRepository ??= new GradeRepository(_context); }
        }

        public IMajorRepository MajorRepository
        {
            get { return _majorRepository ??= new MajorRepository(_context); }
        }

        public INotificationRepository NotificationRepository
        {
            get { return _notificationRepository ??= new NotificationRepository(_context); }
        }

        public IPredictionRepository PredictionRepository
        {
            get { return _predictionRepository ??= new PredictionRepository(_context); }
        }

        public IRiskAnalysisRepository RiskAnalysisRepository
        {
            get { return _riskAnalysisRepository ??= new RiskAnalysisRepository(_context); }
        }

        public ISemesterRepository SemesterRepository
        {
            get { return _semesterRepository ??= new SemesterRepository(_context); }
        }

        public ISubjectRepository SubjectRepository
        {
            get { return _subjectRepository ??= new SubjectRepository(_context); }
        }

        public ISuggestionRepository SuggestionRepository
        {
            get { return _suggestionRepository ??= new SuggestionRepository(_context); }
        }

        public ISyllabusRepository SyllabusRepository
        {
            get { return _syllabusRepository ??= new SyllabusRepository(_context); }
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository ??= new UserRepository(_context); }
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public int SaveChangesWithTransaction()
        {
            using var dbContextTransaction = _context.Database.BeginTransaction();
            try
            {
                int result = _context.SaveChanges();
                dbContextTransaction.Commit();
                return result;
            }
            catch (Exception)
            {
                dbContextTransaction.Rollback();
                return -1;
            }
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            using var dbContextTransaction = _context.Database.BeginTransaction();
            try
            {
                int result = await _context.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await dbContextTransaction.RollbackAsync();
                return -1;
            }
        }
    }
}
