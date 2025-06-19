using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Response
{
    public class SemesterDto
    {
        public Guid SemesterID { get; set; }
        public string SemesterCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
