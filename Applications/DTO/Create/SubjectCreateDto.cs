using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Create
{
    public class SubjectCreateDto
    {
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public Guid ProgramID { get; set; }
        public string Description { get; set; }
    }
}
