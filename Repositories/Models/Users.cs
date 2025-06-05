using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Repositories.Models
{
    public class Users
    {
        [Key]
        public Guid UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid MajorID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Role { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("MajorID")]
        public virtual Majors Major { get; set; }
    }
}