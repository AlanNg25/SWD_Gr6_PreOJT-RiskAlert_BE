using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Response
{
    public class SubjectDto
    {
        public Guid SubjectID { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public Guid ProgramID { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
