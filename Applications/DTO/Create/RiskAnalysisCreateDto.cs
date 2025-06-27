using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Create
{
    public class RiskAnalysisCreateDto
    {
        public Guid EnrollmentID { get; set; }
        public string RiskLevel { get; set; }
        public DateTime? TrackingDate { get; set; }
        public string? Notes { get; set; }
        public bool IsResolved { get; set; }
    }
}
