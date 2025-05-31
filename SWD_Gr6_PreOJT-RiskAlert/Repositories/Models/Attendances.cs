using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class Attendances
    {

        [Key]
        public Guid AttendanceID { get; set; }
        public Guid StudentID { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public Guid SessionID { get; set; }
        public int WeekNumber { get; set; }
        public bool IsDeleted { get; set; }

        public Users Student { get; set; }
        public Sessions Session { get; set; }
    }
}