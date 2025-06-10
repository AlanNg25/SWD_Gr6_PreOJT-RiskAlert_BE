using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using static System.Collections.Specialized.BitVector32;

namespace Repositories.DBContext
{
    public class RiskAlertDBContext : DbContext
    {
        public RiskAlertDBContext() { }
        public RiskAlertDBContext(DbContextOptions<RiskAlertDBContext> options) : base(options) { }
        public DbSet<User> user { get; set; }
        public DbSet<Major> major { get; set; }
        public DbSet<Enrollment> enrollment { get; set; }
        public DbSet<Subject> subject { get; set; }
        public DbSet<Attendance> attendance { get; set; }
        public DbSet<Grade> grade { get; set; }
        public DbSet<GradeDetail> gradeDetail { get; set; }
        public DbSet<Curriculum> curriculum { get; set; }
        public DbSet<Syllabus> syllabus { get; set; }
        public DbSet<Notification> notification { get; set; }
        public DbSet<Prediction> prediction { get; set; }
        public DbSet<RiskAnalysis> riskAnalysis { get; set; }
        public DbSet<Suggestion> suggestion { get; set; }
        public DbSet<Course> course { get; set; }
        public DbSet<Semester> semester { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Major)
                .WithMany(m => m.Enrollments)
                .HasForeignKey(e => e.MajorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Course)
                .WithMany(c => c.Grades)
                .HasForeignKey(g => g.CourseID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Receiver)
                .WithMany(r => r.Notifications)
                .HasForeignKey(n => n.ReceiverID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Suggestion>()
                .HasOne(a => a.Advisor)
                .WithMany(a => a.Suggestions)
                .HasForeignKey(a => a.AdvisorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Suggestion>()
                .HasOne(a => a.Risk)
                .WithMany(r => r.Suggestions)
                .HasForeignKey(a => a.RiskID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Majors)
                .WithMany(m => m.Users)
                .HasForeignKey(u => u.MajorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Curriculum>()
                .HasOne(c => c.Major)
                .WithMany(m => m.Curriculums)
                .HasForeignKey(c => c.MajorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Curriculum>()
                .HasOne(c => c.Subject)
                .WithMany(s => s.Curriculums)
                .HasForeignKey(c => c.SubjectID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prediction>()
                .HasOne(p => p.Student)
                .WithMany(s => s.Predictions)
                .HasForeignKey(p => p.StudentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Syllabus>()
                .HasOne(s => s.Subject)
                .WithMany(s => s.Syllabuses)
                .HasForeignKey(s => s.SubjectID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Syllabus>()
                .HasOne(s => s.Subject)
                .WithMany(s => s.Syllabuses)
                .HasForeignKey(s => s.PrerequisitesSubjectID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Enrollment)
                .WithMany(e => e.Attendances)
                .HasForeignKey(a => a.EnrollmentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GradeDetail>()
                .HasOne(gd => gd.Grade)
                .WithMany(g => g.GradeDetails)
                .HasForeignKey(gd => gd.GradeID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Subject)
                .WithMany(s => s.Courses)
                .HasForeignKey(c => c.SubjectID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Semester)
                .WithMany(s => s.Courses)
                .HasForeignKey(c => c.SemesterID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherID)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
