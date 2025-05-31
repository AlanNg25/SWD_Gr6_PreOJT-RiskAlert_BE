using System.ComponentModel.DataAnnotations;
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

        public Classes Class { get; set; }
        public Users Teacher { get; set; }
        public Subjects Subject { get; set; }
    }
}