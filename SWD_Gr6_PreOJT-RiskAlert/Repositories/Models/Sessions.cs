using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class Sessions
    {
        [Key]
        public Guid SessionID { get; set; }
        public Guid SubjectID { get; set; }
        public int SessionNo { get; set; }
        public DateTime SessionDate { get; set; }
        public string MeetLink { get; set; }
        public bool IsDeleted { get; set; }

        public Subjects Subject { get; set; }
    }
}