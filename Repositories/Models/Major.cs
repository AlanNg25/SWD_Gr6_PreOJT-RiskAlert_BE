using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class Major
    {
        [Key]
        public Guid MajorID { get; set; }
        public string MajorCode { get; set; }
        public string MajorName { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<Curriculum> Curriculums { get; set; } = new List<Curriculum>();
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}