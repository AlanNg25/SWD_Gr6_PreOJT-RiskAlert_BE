using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Response
{
    public class CurriculumDto
    {
        public Guid CurriculumID { get; set; }
        public int TermNo { get; set; }
        public Guid SubjectID { get; set; }
        public Guid MajorID { get; set; }
        public int Year { get; set; }
        public bool IsDeleted { get; set; }
    }
}
