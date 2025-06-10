using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class Notification
    {
        [Key]
        public Guid NotificationID { get; set; }
        public Guid ReceiverID { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public string Attachment { get; set; }
        public DateTime SentTime { get; set; }
        public bool IsDeleted { get; set; }

        public virtual User Receiver { get; set; }
    }
}