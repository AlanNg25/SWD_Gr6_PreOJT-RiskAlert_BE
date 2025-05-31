using System.ComponentModel.DataAnnotations;

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

        public Users Student { get; set; }
        public Subjects Subject { get; set; }

        public ICollection<GradeDetails> GradeDetails { get; set; }
    }
}