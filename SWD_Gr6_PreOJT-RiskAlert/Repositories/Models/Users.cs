using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Repositories.Models
{
    public class Users
    {
        [Key]
        public Guid UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid MajorID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Role { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("MajorID")]
        public Majors Major { get; set; }

        public ICollection<Enrollments> Enrollments { get; set; }
        public ICollection<SubjectInClass> Teachings { get; set; }
        public ICollection<Attendances> Attendances { get; set; }
        public ICollection<Grades> Grades { get; set; }
        public ICollection<Notifications> Notifications { get; set; }
        public ICollection<Predictions> Predictions { get; set; }
    }
}