using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Create
{
    public class EnrollmentCreateDto
    {
        public Guid StudentID { get; set; }
        public Guid MajorID { get; set; }
        public Guid CourseID { get; set; }
        public int EnrollmentStatus { get; set; }
    }
}
