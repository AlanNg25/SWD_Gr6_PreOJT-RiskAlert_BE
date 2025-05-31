using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class Programs
    {
        [Key]
        public Guid ProgramID { get; set; }
        public string ProgramName { get; set; }
        public string Description { get; set; }
        public Guid MajorID { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("MajorID")]
        public Majors Major { get; set; }

        public ICollection<Subjects> Subjects { get; set; }
        public ICollection<Curriculums> Curriculums { get; set; }
    }
}