using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class Notifications
    {
        [Key]
        public Guid NotificationID { get; set; }
        public Guid receiver { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public string Attachment { get; set; }
        public DateTime SentTime { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("receiver")]
        public Users Receiver { get; set; }
    }
}