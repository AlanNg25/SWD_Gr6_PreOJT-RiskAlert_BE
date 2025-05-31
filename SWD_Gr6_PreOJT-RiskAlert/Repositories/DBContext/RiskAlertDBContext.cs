using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;

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
    }
}
