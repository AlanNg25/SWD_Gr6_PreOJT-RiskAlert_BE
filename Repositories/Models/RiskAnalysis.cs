using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models
{
    public class RiskAnalysis
    {
        [Key]
        public Guid RiskID { get; set; }
        public Guid EnrollmentID { get; set; }
        public string RiskLevel { get; set; }
        public DateTime? TrackingDate { get; set; }
        public string? Notes { get; set; }
        public bool IsResolved { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("EnrollmentID")]
        public virtual Enrollments Enrollment { get; set; }
    }
}
