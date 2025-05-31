using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class Majors
    {
        [Key]
        public Guid MajorID { get; set; }
        public string MajorCode { get; set; }
        public string MajorName { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Users> Users { get; set; }
        public ICollection<Programs> Programs { get; set; }
        public ICollection<Enrollments> Enrollments { get; set; }
    }
}