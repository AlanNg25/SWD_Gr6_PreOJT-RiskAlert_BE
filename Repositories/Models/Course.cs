using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class Course
    {
        [Key]
        public Guid CourseID { get; set; }
        public string CourseCode { get; set; }
        public Guid TeacherID { get; set; }
        public Guid SemesterID { get; set; }
        public Guid SubjectID { get; set; }
        public bool IsDeleted { get; set; }
        public virtual User Teacher { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}