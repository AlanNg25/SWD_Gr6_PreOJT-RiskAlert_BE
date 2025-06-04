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
        public DbSet<Attendances> attendances { get; set; }
        public DbSet<Grades> grades { get; set; }
        public DbSet<GradeDetails> gradeDetails { get; set; }
        public DbSet<Programs> programs { get; set; }
        public DbSet<Curriculums> curriculums { get; set; }
        public DbSet<Syllabus> syllabus { get; set; }
        public DbSet<Notifications> notifications { get; set; }
        public DbSet<Predictions> predictions { get; set; }
        public DbSet<RiskAnalysis> riskAnalysis { get; set; }
        public DbSet<Actions> actions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.Student)
                .WithMany()
                .HasForeignKey(e => e.StudentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grades>()
                .HasOne(g => g.Student)
                .WithMany()
                .HasForeignKey(g => g.StudentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notifications>()
                .HasOne(n => n.Receiver)
                .WithMany()
                .HasForeignKey(n => n.ReceiverID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubjectInClass>()
                .HasOne(sic => sic.Teacher)
                .WithMany()
                .HasForeignKey(sic => sic.TeacherID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Actions>()
                .HasOne(a => a.Advisor)
                .WithMany()
                .HasForeignKey(a => a.AdvisorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Major)
                .WithMany()
                .HasForeignKey(u => u.MajorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.Major)
                .WithMany()
                .HasForeignKey(e => e.MajorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Programs>()
                .HasOne(p => p.Major)
                .WithMany()
                .HasForeignKey(p => p.MajorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subjects>()
                .HasOne(s => s.Program)
                .WithMany()
                .HasForeignKey(s => s.ProgramID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Curriculums>()
                .HasOne(c => c.Program)
                .WithMany()
                .HasForeignKey(c => c.ProgramID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Curriculums>()
                .HasOne(c => c.Subject)
                .WithMany()
                .HasForeignKey(c => c.SubjectID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubjectInClass>()
                .HasOne(sic => sic.Subject)
                .WithMany()
                .HasForeignKey(sic => sic.SubjectID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grades>()
                .HasOne(g => g.Subject)
                .WithMany()
                .HasForeignKey(g => g.SubjectID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Predictions>()
                .HasOne(p => p.Student)
                .WithMany()
                .HasForeignKey(p => p.StudentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Syllabus>()
                .HasOne(s => s.Subject)
                .WithMany()
                .HasForeignKey(s => s.SubjectID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Syllabus>()
                .HasOne(s => s.Subject)
                .WithMany()
                .HasForeignKey(s => s.PrerequisitesSubjectID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.Class)
                .WithMany()
                .HasForeignKey(e => e.ClassID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubjectInClass>()
                .HasOne(sic => sic.Class)
                .WithMany()
                .HasForeignKey(sic => sic.ClassID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendances>()
                .HasOne(a => a.Enrollment)
                .WithMany()
                .HasForeignKey(a => a.EnrollmentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendances>()
                .HasOne(a => a.ClassSubject)
                .WithMany()
                .HasForeignKey(a => a.ClassSubjectID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Actions>()
                .HasOne(a => a.Risk)
                .WithMany()
                .HasForeignKey(a => a.RiskID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GradeDetails>()
                .HasOne(gd => gd.Grade)
                .WithMany()
                .HasForeignKey(gd => gd.GradeID)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
