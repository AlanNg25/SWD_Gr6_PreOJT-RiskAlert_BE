using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Create
{
    public class NotificationCreateDto
    {
        public Guid ReceiverID { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public string Attachment { get; set; }
        public DateTime SentTime { get; set; }
    }
}
