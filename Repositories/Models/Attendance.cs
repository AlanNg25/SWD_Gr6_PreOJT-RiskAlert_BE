using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Models
{
    public class Attendance//sort theo Enrollment
    {

        [Key]
        public Guid AttendanceID { get; set; }
        public Guid EnrollmentID { get; set; }
        public int AttendNumber { get; set; }
        public int SessionNumber { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Enrollment Enrollment { get; set; }
    }
}