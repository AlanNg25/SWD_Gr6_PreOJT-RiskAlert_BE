using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Response
{
    public class MajorDto
    {
        public Guid MajorID { get; set; }
        public string MajorCode { get; set; }
        public string MajorName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
