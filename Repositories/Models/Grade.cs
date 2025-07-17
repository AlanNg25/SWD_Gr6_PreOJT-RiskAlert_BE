using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Models
{
    public class Grade
    {
        [Key]
        public Guid GradeID { get; set; }
        public Guid StudentID { get; set; }
        public Guid CourseID { get; set; }
        public DateTime GradeDate { get; set; }///
        public decimal ScoreAverage { get; set; }
        public bool IsDeleted { get; set; }

        public virtual User Student { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<GradeDetail> GradeDetails { get; set; } = new List<GradeDetail>();
    }
}