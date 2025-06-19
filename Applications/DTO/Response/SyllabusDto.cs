using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Response
{
    public class SyllabusDto
    {
        public Guid SyllabusID { get; set; }
        public Guid SubjectID { get; set; }
        public string SyllabusName { get; set; }
        public Guid? PrerequisitesSubjectID { get; set; }
        public bool IsApproved { get; set; }
        public string Description { get; set; }
        public string StudentTasks { get; set; }
        public string ScoringCondition { get; set; }
        public string Type { get; set; }
        public int CreditHours { get; set; }
        public bool IsDeleted { get; set; }
    }
}
