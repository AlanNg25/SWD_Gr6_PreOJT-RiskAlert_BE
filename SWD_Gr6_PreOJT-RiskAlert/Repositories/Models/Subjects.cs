using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Repositories.Models
{
    public class Subjects
    {
        [Key]
        public Guid SubjectID { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public Guid ProgramID { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ProgramID")]
        public Programs Program { get; set; }

        public ICollection<Curriculums> Curriculums { get; set; }
        public ICollection<Syllabus> Syllabi { get; set; }
        public ICollection<SubjectInClass> SubjectInClasses { get; set; }
        public ICollection<Sessions> Sessions { get; set; }
        public ICollection<Grades> Grades { get; set; }
        public ICollection<Predictions> Predictions { get; set; }
    }
}