using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("ProgramID")]
        public virtual Programs Program { get; set; }
        [ForeignKey("SubjectID")]
        public virtual Subjects Subject { get; set; }
    }
}