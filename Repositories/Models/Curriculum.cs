using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Models
{
    public class Curriculum
    {
        [Key]
        public Guid CurriculumID { get; set; }
        public int TermNo { get; set; }
        public Guid SubjectID { get; set; }
        public Guid MajorID { get; set; }
        public int Year { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Major Major { get; set; }
        public virtual Subject Subject { get; set; }
    }
}