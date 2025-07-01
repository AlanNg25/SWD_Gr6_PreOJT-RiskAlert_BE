using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Response
{
    public class GradeWithCourseSemesterDto
    {
        public Guid GradeID { get; set; }
        public Guid StudentID { get; set; }
        public Guid CourseID { get; set; }
        public DateTime GradeDate { get; set; }
        public decimal ScoreAverage { get; set; }
        public bool IsDeleted { get; set; }

        public CourseDto Course { get; set; }
        public SemesterDto Semester { get; set; }
    }

}
