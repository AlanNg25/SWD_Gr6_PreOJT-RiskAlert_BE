using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Response
{
    public class EnrollmentDto
    {
        public Guid EnrollmentID { get; set; }
        public Guid StudentID { get; set; }
        public Guid MajorID { get; set; }
        public Guid CourseID { get; set; }
        public int EnrollmentStatus { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
