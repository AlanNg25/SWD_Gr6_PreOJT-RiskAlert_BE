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
        public DbSet<Users> users { get; set; }
        public DbSet<Majors> majors { get; set; }
        public DbSet<Enrollments> enrollments { get; set; }
        public DbSet<Classes> classes { get; set; }
        public DbSet<Subjects> subjects { get; set; }
        public DbSet<SubjectInClass> subjectInClasses { get; set; }
        public DbSet<Sessions> sessions { get; set; }
        public DbSet<Attendances> attendances { get; set; }
        public DbSet<Grades> grades { get; set; }
        public DbSet<GradeDetails> gradeDetails { get; set; }
        public DbSet<Programs> programs { get; set; }
        public DbSet<Curriculums> curriculums { get; set; }
        public DbSet<Syllabus> syllabus { get; set; }
        public DbSet<Notifications> notifications { get; set; }
        public DbSet<Predictions> predictions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Majors>(entity =>
            {
                entity.HasKey(e => e.MajorID);
                entity.Property(e => e.MajorCode).HasMaxLength(50);
                entity.Property(e => e.MajorName).HasMaxLength(100);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserID);
                entity.Property(e => e.FullName).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Role).HasMaxLength(50);

                entity.HasOne(e => e.Major)
                      .WithMany()
                      .HasForeignKey(e => e.MajorID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Programs>(entity =>
            {
                entity.HasKey(e => e.ProgramID);
                entity.Property(e => e.ProgramName).HasMaxLength(200);
                entity.HasOne(e => e.Major)
                      .WithMany()
                      .HasForeignKey(e => e.MajorID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Subjects>(entity =>
            {
                entity.HasKey(e => e.SubjectID);
                entity.Property(e => e.SubjectCode).HasMaxLength(50);
                entity.Property(e => e.SubjectName).HasMaxLength(100);

                entity.HasOne(e => e.Program)
                      .WithMany()
                      .HasForeignKey(e => e.ProgramID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Curriculums>(entity =>
            {
                entity.HasKey(e => e.CurriculumID);
                entity.Property(e => e.Term).HasMaxLength(10);

                entity.HasOne(e => e.Program)
                      .WithMany()
                      .HasForeignKey(e => e.ProgramID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Subject)
                      .WithMany()
                      .HasForeignKey(e => e.SubjectID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Syllabus>(entity =>
            {
                entity.HasKey(e => e.SyllabusID);
                entity.Property(e => e.SyllabusName).HasMaxLength(100);
                entity.Property(e => e.StudentTasks).HasMaxLength(50);
                entity.Property(e => e.ScoringCondition).HasMaxLength(50);
                entity.Property(e => e.TYPES).HasMaxLength(50);

                entity.HasOne(e => e.Subject)
                      .WithMany()
                      .HasForeignKey(e => e.SubjectID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Prerequisite)
                      .WithMany()
                      .HasForeignKey(e => e.PrerequisitesSubjectID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Classes>(entity =>
            {
                entity.HasKey(e => e.ClassID);
                entity.Property(e => e.ClassCode).HasMaxLength(50);
                entity.Property(e => e.ClassName).HasMaxLength(100);
            });

            modelBuilder.Entity<SubjectInClass>(entity =>
            {
                entity.HasKey(e => e.SubjectInClassID);

                entity.HasOne(e => e.Class)
                      .WithMany()
                      .HasForeignKey(e => e.ClassID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Teacher)
                      .WithMany()
                      .HasForeignKey(e => e.TeacherID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Subject)
                      .WithMany()
                      .HasForeignKey(e => e.SubjectID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Enrollments>(entity =>
            {
                entity.HasKey(e => e.EnrollmentID);

                entity.HasOne(e => e.Student)
                      .WithMany()
                      .HasForeignKey(e => e.StudentID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Class)
                      .WithMany()
                      .HasForeignKey(e => e.ClassID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Major)
                      .WithMany()
                      .HasForeignKey(e => e.MajorID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Sessions>(entity =>
            {
                entity.HasKey(e => e.SessionID);

                entity.HasOne(e => e.Subject)
                      .WithMany()
                      .HasForeignKey(e => e.SubjectID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Attendances>(entity =>
            {
                entity.HasKey(e => e.AttendanceID);
                entity.Property(e => e.Status).HasMaxLength(50);
                entity.Property(e => e.Note).HasMaxLength(200);

                entity.HasOne(e => e.Student)
                      .WithMany()
                      .HasForeignKey(e => e.StudentID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Session)
                      .WithMany()
                      .HasForeignKey(e => e.SessionID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Grades>(entity =>
            {
                entity.HasKey(e => e.GradeID);

                entity.HasOne(e => e.Student)
                      .WithMany()
                      .HasForeignKey(e => e.StudentID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Subject)
                      .WithMany()
                      .HasForeignKey(e => e.SubjectID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GradeDetails>(entity =>
            {
                entity.HasKey(e => e.GradeDetailID);
                entity.Property(e => e.GradeType).HasMaxLength(50);

                entity.HasOne(e => e.Grade)
                      .WithMany()
                      .HasForeignKey(e => e.GradeID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.HasKey(e => e.NotificationID);
                entity.Property(e => e.Attachment).HasMaxLength(200);

                entity.HasOne(e => e.Receiver)
                      .WithMany()
                      .HasForeignKey(e => e.ReceiverID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Predictions>(entity =>
            {
                entity.HasKey(e => e.PredictionID);
                entity.Property(e => e.RiskLevel).HasMaxLength(50);

                entity.HasOne(e => e.Student)
                      .WithMany()
                      .HasForeignKey(e => e.StudentID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Subject)
                      .WithMany()
                      .HasForeignKey(e => e.SubjectID)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
