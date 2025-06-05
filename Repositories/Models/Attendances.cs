using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Models
{
    public class Attendances
    {

        [Key]
        public Guid AttendanceID { get; set; }
        public Guid EnrollmentID { get; set; }
        public Guid ClassSubjectID { get; set; }
        public int AttendNumber { get; set; }
        public int SessionNumber { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("EnrollmentID")]
        public virtual Enrollments Enrollment { get; set; }
        [ForeignKey("ClassSubjectID")]
        public virtual SubjectInClass ClassSubject { get; set; }
    }
}