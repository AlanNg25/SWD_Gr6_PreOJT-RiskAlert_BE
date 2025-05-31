using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class Curriculums
    {
        [Key]
        public Guid CurriculumID { get; set; }
        public string Term { get; set; }
        public Guid ProgramID { get; set; }
        public Guid SubjectID { get; set; }
        public int Year { get; set; }
        public bool IsDeleted { get; set; }

        public Programs Program { get; set; }
        public Subjects Subject { get; set; }
    }
}