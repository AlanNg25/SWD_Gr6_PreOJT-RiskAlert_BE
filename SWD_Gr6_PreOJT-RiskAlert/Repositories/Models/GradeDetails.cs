using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Repositories.Models
{
    public class GradeDetails
    {
        [Key]
        public Guid GradeDetailID { get; set; }
        public Guid GradeID { get; set; }
        public string GradeType { get; set; }
        public decimal Score { get; set; }
        public decimal ScoreWeight { get; set; }
        public decimal MinScr { get; set; }
        public bool IsDeleted { get; set; }

        public Grades Grade { get; set; }
    }
}