using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Models
{
    public class Grades
    {
        [Key]
        public Guid GradeID { get; set; }
        public Guid StudentID { get; set; }
        public Guid SubjectID { get; set; }
        public DateTime GradeDate { get; set; }
        public decimal Score { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("StudentID")]
        public virtual Users Student { get; set; }
        [ForeignKey("SubjectID")]
        public virtual Subjects Subject { get; set; }
    }
}