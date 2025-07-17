using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Repositories.Models
{
    public class Enrollment
    {
        [Key]
        public Guid EnrollmentID { get; set; }
        public Guid StudentID { get; set; }
        public Guid MajorID { get; set; }
        public Guid CourseID { get; set; }
        public int EnrollmentStatus { get; set; }
        public DateTime EnrollmentDate { get; set; }///////////
        public bool IsDeleted { get; set; }


        public virtual User Student { get; set; }
        public virtual Course Course { get; set; }
        public virtual Major Major { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public virtual ICollection<RiskAnalysis> RiskAnalyses { get; set; } = new List<RiskAnalysis>();
    }
}