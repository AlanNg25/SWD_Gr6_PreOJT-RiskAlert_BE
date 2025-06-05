using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models
{
    public class Actions
    {
        [Key]
        public Guid ActionID { get; set; }
        public Guid RiskID { get; set; }
        public Guid AdvisorID { get; set; }
        public DateTime? SentDate { get; set; }
        public DateTime? ActionDate { get; set; }
        public string? ActionType { get; set; }
        public string? Notes { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("RiskID")]
        public virtual RiskAnalysis Risk { get; set; }
        [ForeignKey("AdvisorID")]
        public virtual Users Advisor { get; set; }
    }
}
