using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Create
{
    public class SuggestionCreateDto
    {
        public Guid RiskID { get; set; }
        public Guid AdvisorID { get; set; }
        public DateTime? SentDate { get; set; }
        public DateTime? ActionDate { get; set; }
        public string? ActionType { get; set; }
        public string? Notes { get; set; }
    }
}
