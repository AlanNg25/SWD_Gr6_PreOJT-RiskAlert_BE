using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Create
{
    public class CourseCreateDto
    {
        public string CourseCode { get; set; }
        public Guid TeacherID { get; set; }
        public Guid SemesterID { get; set; }
        public Guid SubjectID { get; set; }
    }
}
