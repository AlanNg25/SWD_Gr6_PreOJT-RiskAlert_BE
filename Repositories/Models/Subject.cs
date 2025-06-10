using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Repositories.Models
{
    public class Subject
    {
        [Key]
        public Guid SubjectID { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public Guid ProgramID { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        public virtual ICollection<Syllabus> Syllabuses { get; set; } = new List<Syllabus>();
        public virtual ICollection<Curriculum> Curriculums { get; set; } = new List<Curriculum>();
    }
}