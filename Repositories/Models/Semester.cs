using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class Semester
    {
        [Key]
        public Guid SemesterID { get; set; }
        public string SemesterCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}