using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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
        public string Status { get; set; }
        public bool IsDeleted { get; set; }

        public Users Student { get; set; }
        public Classes Class { get; set; }
        public Majors Major { get; set; }
    }
}