using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Repositories.Models
{
    public class Enrollments
    {
        [Key]
        public Guid EnrollmentID { get; set; }
        public Guid StudentID { get; set; }
        public Guid ClassID { get; set; }
        public Guid MajorID { get; set; }
        public int EnrollmentStatus { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("StudentID")]
        public virtual Users Student { get; set; }
        [ForeignKey("ClassID")]
        public virtual Classes Class { get; set; }
        [ForeignKey("MajorID")]
        public virtual Majors Major { get; set; }
    }
}