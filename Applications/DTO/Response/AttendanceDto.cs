using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Response
{
    public class AttendanceDto
    {
        public Guid AttendanceID { get; set; }
        public Guid EnrollmentID { get; set; }
        public int AttendNumber { get; set; }
        public int SessionNumber { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }
    }
}
