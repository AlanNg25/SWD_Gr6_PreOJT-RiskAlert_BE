using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class Syllabus
    {
        [Key]
        public Guid SyllabusID { get; set; }
        public Guid SubjectID { get; set; }
        public string SyllabusName { get; set; }
        public Guid? PrerequisitesSubjectID { get; set; }
        public bool IsApproved { get; set; }
        public string Description { get; set; }
        public string StudentTasks { get; set; }
        public string ScoringCondition { get; set; }
        public string TYPES { get; set; }
        public int CreditHours { get; set; }
        public bool IsDeleted { get; set; }

        public Subjects Subject { get; set; }
        public Syllabus Prerequisite { get; set; }
    }
}