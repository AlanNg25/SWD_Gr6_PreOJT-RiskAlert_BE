using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models
{
    public class Suggestion
    {
        [Key]
        public Guid SuggestionID { get; set; }
        public Guid RiskID { get; set; }
        public Guid AdvisorID { get; set; }
        public DateTime? SentDate { get; set; }/// 
        public DateTime? ActionDate { get; set; }
        public string? ActionType { get; set; }
        public string? Notes { get; set; }
        public bool IsDeleted { get; set; }

        public virtual RiskAnalysis Risk { get; set; }
        public virtual User Advisor { get; set; }
    }
}
