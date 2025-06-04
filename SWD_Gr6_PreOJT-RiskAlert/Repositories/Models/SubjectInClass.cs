using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace Repositories.Models
{
    public class SubjectInClass
    {
        [Key]
        public Guid SubjectInClassID { get; set; }
        public Guid ClassID { get; set; }
        public Guid TeacherID { get; set; }
        public Guid SubjectID { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ClassID")]
        public virtual Classes Class { get; set; }
        [ForeignKey("TeacherID")]
        public virtual Users Teacher { get; set; }
        [ForeignKey("SubjectID")]
        public virtual Subjects Subject { get; set; }
    }
}