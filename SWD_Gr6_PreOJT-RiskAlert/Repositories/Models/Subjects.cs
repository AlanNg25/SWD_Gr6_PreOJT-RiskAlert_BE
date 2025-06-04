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
        public virtual Programs Program { get; set; }
    }
}