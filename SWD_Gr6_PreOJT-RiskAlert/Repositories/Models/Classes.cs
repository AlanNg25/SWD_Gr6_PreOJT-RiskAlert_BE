using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class Classes
    {
        [Key]
        public Guid ClassID { get; set; }
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<SubjectInClass> SubjectInClasses { get; set; }
        public ICollection<Enrollments> Enrollments { get; set; }
    }
}